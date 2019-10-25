using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IImageService
    {
        List<string> Validate(IFormFileCollection files);
        Task UploadAsync(IFormFileCollection files, long Id);

    }
}
