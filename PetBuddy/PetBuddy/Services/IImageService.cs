using Microsoft.AspNetCore.Http;
using PetBuddy.Models;
using PetBuddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IImageService
    {
        List<string> Validate(IFormFile file, PlaceInfoViewModel newPlace);
        Task UploadAsync(IFormFile file, string Id, string blobContainerName);
        Task<List<ImageDetails>> ListAsync(string Id, string blobContainerName);
    }
}
