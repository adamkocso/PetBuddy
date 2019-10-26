using System.Collections.Generic;
using System.Threading.Tasks;
using PetBuddy.Models;
using PetBuddy.Viewmodels;

namespace PetBuddy.Services
{
    public interface IPetService
    {
        Task AddPetAsync(PetViewModel newPet, User user);
        Task<List<Pet>> MyPetsAsync(User user);
        Task DeletePetAsync(long petId);
        Task<Pet> FindPetByIdAsync(long petId);
        Task EditPetAsync(PetViewModel editPet, long petId);
    }
}