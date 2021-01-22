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
        Task<Dictionary<AdType, int>> GetViewsPerType();
        Task<List<AdCategory>> TopCategories(int quantity);
        Task<List<Ad>> TopAds(int quantity);

    }
}
