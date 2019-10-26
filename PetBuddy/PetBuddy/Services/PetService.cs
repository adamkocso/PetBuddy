using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public class PetService : IPetService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IImageService imageService;

        public PetService(ApplicationContext applicationContext, IImageService imageService)
        {
            this.applicationContext = applicationContext;
            this.imageService = imageService;
        }

        public async Task<long> AddPetAsync(PetViewModel newPet, User user)
        {
            var pet = new Pet
            {
                PetName = newPet.PetName,
                PetType = newPet.PetType,
                PetDescription = newPet.PetDescription,
                UserId = user.Id
            };
            await applicationContext.Pets.AddAsync(pet);
            await applicationContext.SaveChangesAsync();
            return pet.PetId;
        }

        public async Task<List<Pet>> MyPetsAsync(User user)
        {
            var pets = await applicationContext.Pets.Where(p => p.UserId == user.Id).ToListAsync();
            return pets;
        }

        public async Task DeletePetAsync(long petId)
        {
            var pet = await applicationContext.Pets.FirstOrDefaultAsync(p => p.PetId == petId);
            applicationContext.Pets.Remove(pet);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Pet> FindPetByIdAsync(long petId)
        {
            var pet = await applicationContext.Pets.FirstOrDefaultAsync(p => p.PetId == petId);
            return pet;
        }

        public async Task EditPetAsync(PetViewModel editPet, long petId)
        {
            var pet = await FindPetByIdAsync(petId);
            pet.PetName = editPet.PetName;
            pet.PetType = editPet.PetType;
            pet.PetDescription = editPet.PetDescription;
            applicationContext.Pets.Update(pet);
            await applicationContext.SaveChangesAsync();
        }

        public async Task SetIndexImageAsync(long petId, string blobContainerName)
        {
            var pet = await FindPetByIdAsync(petId);
            var pictures = await imageService.ListAsync(petId.ToString(), blobContainerName);
            pet.PetUri = pictures[0].Path;
            applicationContext.Pets.Update(pet);
            await applicationContext.SaveChangesAsync();
        }
    }
}