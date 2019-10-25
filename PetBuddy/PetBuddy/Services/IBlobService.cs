using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    interface IBlobService
    {
        Task<CloudBlobContainer> GetBlobContainer(string blobContainerName);
    }
}
