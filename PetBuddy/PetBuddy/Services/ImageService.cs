using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using PetBuddy.Models;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public class ImageService : IImageService
    {
        private readonly IBlobService blobService;
        private int fourMegaByte = 4 * 1024 * 1024;
        private readonly string[] validExtensions = { "jpg", "png", "jpeg" };

        public ImageService(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        public List<string> Validate(IFormFile file, PlaceInfoViewModel newPlace)
        {
            if (CheckImageExtension(file))
            {
                if (file.Length < fourMegaByte)
                {
                    return newPlace.ErrorMessages;
                }
                else
                {
                    newPlace.ErrorMessages.Add("The image max 4 MB");
                    return newPlace.ErrorMessages;
                }
            }
            else
            {
                newPlace.ErrorMessages.Add("Please add only image formats!");
                return newPlace.ErrorMessages;
            }

            return newPlace.ErrorMessages;
        }
        
        
        private bool CheckImageExtension(IFormFile file)
        {
            var fileNameSegments = file.FileName.Split(".");
            var extensions = new List<string>(validExtensions);
            return extensions.Contains(fileNameSegments[fileNameSegments.Length - 1]);
        }

        public async Task UploadAsync(IFormFile file, long Id, string blobContainerName)
        {
            var blobcontainer = await blobService.GetBlobContainer(blobContainerName);
            var blob = blobcontainer.GetBlockBlobReference(Id + "/" + file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }
        }

        public async Task<List<ImageDetails>> ListAsync(long placeId, string blobContainerName)
        {
            var imageList = new List<ImageDetails>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var blobContainer = await blobService.GetBlobContainer(blobContainerName);
                var response = await blobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = response.ContinuationToken;
                await GetBlobDirectoryAsync(imageList, placeId, blobContainerName);
            } while (blobContinuationToken != null);

            return imageList;
        }

        private async Task GetBlobDirectoryAsync(List<ImageDetails> imageList, long hotelId, string blobContainerName)
        {
            var blobContainer = await blobService.GetBlobContainer(blobContainerName);
            foreach (var item in blobContainer.ListBlobs())
            {
                if (item is CloudBlobDirectory)
                {
                    GetImagesFromBlobs(item, imageList, hotelId);
                }
            }
        }

        private void GetImagesFromBlobs(IListBlobItem item, List<ImageDetails> imageList, long hotelId)
        {
            CloudBlobDirectory directory = (CloudBlobDirectory)item;
            IEnumerable<IListBlobItem> blobs = directory.ListBlobs(true);
            foreach (var blob in blobs)
            {
                var folderId = GetFolders(blob.Uri);
                if (hotelId == folderId)
                {
                    imageList.Add(new ImageDetails
                    {
                        Name = blob.Uri.Segments[blob.Uri.Segments.Length - 1],
                        Path = blob.Uri.ToString()
                    });
                }
            }
        }

        private static int GetFolders(Uri uri)
        {
            var path = uri.ToString().Split("/");
            var folder = path[path.Length - 2];
            return Convert.ToInt32(folder);
        }
    }
}
