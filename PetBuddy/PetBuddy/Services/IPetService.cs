using System.Collections.Generic;
using System.Threading.Tasks;
using PetBuddy.Models;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public interface IPetService
    {
        Task<long> AddPetAsync(PetViewModel newPet, User user);
        Task<List<Pet>> MyPetsAsync(User user);
        Task DeletePetAsync(long petId);
        Task<Pet> FindPetByIdAsync(long petId);
        Task EditPetAsync(PetViewModel editPet, long petId);
        Task SetIndexImageAsync(long petId, string blobContainerName);
    }
}