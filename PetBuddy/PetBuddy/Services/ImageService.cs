using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PetBuddy.Services
{
    public class ImageService : IImageService
    {
        //private readonly IBlobService blobService;
        //private int fourMegaByte = 4 * 1024 * 1024;
        //private readonly string[] validExtensions = { "jpg", "png", "jpeg" };

        //public List<string> Validate(IFormFileCollection files)
        //{
        //    foreach (var file in files)
        //    {
        //        if (CheckImageExtension(file))
        //        {
        //            if (file.Length < fourMegaByte)
        //            {
        //                return newHotel.ErrorMessages;
        //            }
        //            else
        //            {
        //                newHotel.ErrorMessages.Add("The image max 4 MB");
        //                return newHotel.ErrorMessages;
        //            }
        //        }
        //        else
        //        {
        //            newHotel.ErrorMessages.Add("Please add only image formats!");
        //            return newHotel.ErrorMessages;
        //        }
        //    }

        //    return newHotel.ErrorMessages;
        //}
        //private bool CheckImageExtension(IFormFile file)
        //{
        //    var fileNameSegments = file.FileName.Split(".");
        //    var extensions = new List<string>(validExtensions);
        //    return extensions.Contains(fileNameSegments[fileNameSegments.Length - 1]);
        //}

        //public async Task UploadAsync(IFormFile file, long Id, string blobContainerName)
        //{
        //    var blobcontainer = await blobService.GetBlobContainer(blobContainerName);
        //    var blob = blobcontainer.GetBlockBlobReference(Id + "/" + file.FileName);
        //    using (var stream = file.OpenReadStream())
        //    {
        //        await blob.UploadFromStreamAsync(stream);
        //    }
        //}
    }
}
