using System.Net.NetworkInformation;

namespace Wpm.Clinic.ExternalServices
{
    public class ManagementService
    {
        private static HttpClient _httpClient;
        public ManagementService(HttpClient client) { _httpClient = client; }
        public async Task<PetInfo> GetPetInfo(int id) { var petInfo  = await _httpClient.GetFromJsonAsync<PetInfo>($"/api/pets/{id}");  return petInfo; }
    }
   public record PetInfo(int id, string Name, int Age, int BreedId) { }
}
