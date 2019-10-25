using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PetBuddy.Services
{
    public class ImageService : IImageService
    {
        private readonly IBlobService blobService;
        private int fourMegaByte = 4 * 1024 * 1024;
        private readonly string[] validExtensions = { "jpg", "png" };

        public List<string> Validate(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                if (CheckImageExtension(file))
                {
                    if (file.Length < fourMegaByte)
                    {
                        return newHotel.ErrorMessages;
                    }
                    else
                    {
                        newHotel.ErrorMessages.Add("The image max 4 MB");
                        return newHotel.ErrorMessages;
                    }
                }
                else
                {
                    newHotel.ErrorMessages.Add("Please add only image formats!");
                    return newHotel.ErrorMessages;
                }
            }

            return newHotel.ErrorMessages;
        }
        private bool CheckImageExtension(IFormFile file)
        {
            var fileNameSegments = file.FileName.Split(".");
            var extensions = new List<string>(validExtensions);
            return extensions.Contains(fileNameSegments[fileNameSegments.Length - 1]);
        }

        public async Task UploadAsync(IFormFileCollection files, long hotelId, string )
        {
            var blobcontainer = await blobService.GetBlobContainer(string blobcontainer);
            for (int i = 0; i < files.Count; i++)
            {
                var blob = blobcontainer.GetBlockBlobReference(hotelId + "/" + files[i].FileName);
                using (var stream = files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
            }
        }
    }
}
