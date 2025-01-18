using System.Net.Cache;
using Wpm.Management.Api.DataAccess;

namespace Wpm.Management.Api.Model
{
    public record NewBreed(string Name)
    {
        public Breed ToBreed()
        {
            return new Breed(0,Name)  ;
        }
    }
}
