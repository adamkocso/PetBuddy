using System.Collections.Generic;
using System.Threading.Tasks;
using PetBuddy.Models;

namespace PetBuddy.Services
{
    public interface IHomeService
    {
        List<Place> FindAllPlacesAsync();
    }
}