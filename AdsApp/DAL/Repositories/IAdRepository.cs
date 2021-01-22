using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAdRepository
    {
        Task CreateAsync(Ad ad);
        Task<Ad> UpdateAsync(Ad ad);
        Task DeleteAsync(int id);
        Task<Ad> GetNextAsync(AdType? type, AdCategory? category, int lastShownAdId = 0);
        Task<Dictionary<AdType, int>> GetViewsPerTypeAsync();
        Task<List<AdCategory>> TopCategoriesAsync(int quantity);
        Task<List<Ad>> TopAdsAsync(int quantity);
    }
}
