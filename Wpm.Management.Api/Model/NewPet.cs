using System.Net.Cache;
using Wpm.Management.Api.DataAccess;

namespace Wpm.Management.Api.Model
{
    public record NewPet(string Name, int Age, int BreedId)
    {
        public Pet ToPet()
        {
            return new Pet() { Name=Name,Age=Age, BreedId=BreedId};
        }
    }
}
