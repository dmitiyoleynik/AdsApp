using BL.DTO;
using BL.Models;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAdService
    {
        Task CreateAdAsync(Ad ad);
        Task<AdResponse> GetAdByTokenAsync(string token);
        Task<AdResponse> GetAdWithNoTokenAsync(AdType? type, AdCategory? category);
        Task DeleteAdAsync(int id);
        Task<Ad> UpdateAdAsync(Ad ad);
        Task<Statistics> GetStatistics();
    }
}
