using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAdRepository
    {
        Task CreateAsync(Models.Ad ad);
        Task<Ad> UpdateAsync(Models.Ad ad);
        Task DeleteAsync(int id);
        Task<Ad> GetNextAsync( AdType? type, AdCategory? category, int lastShownAdId=0);
        Task<Dictionary<AdType, int>> GetViewsPerType();
        Task<List<AdCategory>> TopCategories(int quantity);
        Task<List<Ad>> TopAds(int quantity);

    }
}
