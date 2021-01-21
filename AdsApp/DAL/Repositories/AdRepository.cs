using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AdRepository : IAdRepository
    {
        private readonly ApplicationDBContext _context;
        public AdRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CreateAsync(Models.Ad ad)
        {
            _context.Ads.Add(new EFModels.Ad
            {
                Category = ad.Category,
                Type = ad.Type,
                Cost = ad.Cost,
                Content = ad.Content,
                IsActive = ad.IsActive,
                Updated = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return _context.Ads.FirstOrDefault(x => x.Content == ad.Content && x.Type == ad.Type && x.Category == ad.Category && x.Cost == ad.Cost).Id;
        }

        public async Task DeleteAsync(int id)
        {
            var ad = await _context.Ads.FirstAsync(x => x.Id == id && x.Deleted == null);

            ad.Deleted = DateTime.Now;
            ad.Updated = DateTime.Now;

            _context.Ads.Update(ad);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Ad> GetNextAsync(AdType? type, AdCategory? category, int lastShownAdId = 0)
        {
            var query = AddFilters(_context.Ads.Where(x => x.Deleted == null), type, category);
            var ad = await query.FirstAsync(x => x.IsActive && x.Id > lastShownAdId);
            ad.Views++;

            _context.Ads.Update(ad);
            await _context.SaveChangesAsync();

            return new Models.Ad { Id = ad.Id, Type = ad.Type, Category = ad.Category, Cost = ad.Cost, Content = ad.Content, IsActive = ad.IsActive };
        }

        public async Task<Dictionary<AdType, int>> GetViewsPerType()
        {
            var dict = new Dictionary<AdType, int>();
            var viewsPerType = await _context.Ads.Where(x => x.Deleted == null)
                .Where(x => x.IsActive)
                .GroupBy(x => x.Type)
                .Select(g => new { Type = g.Key, Views = g.Sum(x => x.Views) })
                .ToListAsync();

            foreach (var viewPerType in viewsPerType)
            {
                dict.Add(viewPerType.Type, viewPerType.Views);
            }

            return dict;
        }

        public async Task<List<Models.Ad>> TopAds(int quantity)
        {
            var ads = await _context.Ads.Where(x => x.Deleted == null)
                .Where(x => x.IsActive)
                .OrderBy(x => x.Views)
                .Take(quantity)
                .Select(x => new Models.Ad { Type = x.Type, Category = x.Category, Content = x.Content, Cost = x.Cost, IsActive = x.IsActive })
                .ToListAsync();

            return ads;
        }

        public async Task<List<AdCategory>> TopCategories(int quantity)
        {
            var categories = await _context.Ads.Where(x => x.Deleted == null)
                .Where(x => x.IsActive)
                .GroupBy(x => x.Category)
                .Select(g => new { Categry = g.Key, Views = g.Sum(x => x.Views) })
                .OrderBy(x => x.Views)
                .Take(3)
                .Select(x => x.Categry)
                .ToListAsync();

            return categories;
        }

        public async Task<Models.Ad> UpdateAsync(Models.Ad ad)
        {
            var oldAd = await _context.Ads.FirstAsync(x => x.Id == ad.Id && x.Deleted == null);

            oldAd.Type = ad.Type;
            oldAd.Category = ad.Category;
            oldAd.Content = ad.Content;
            oldAd.Cost = ad.Cost;
            oldAd.IsActive = ad.IsActive;
            oldAd.Updated = DateTime.Now;

            _context.Ads.Update(oldAd);
            await _context.SaveChangesAsync();

            return ad;
        }

        private IQueryable<EFModels.Ad> AddFilters(IQueryable<EFModels.Ad> query, AdType? type, AdCategory? category)
        {
            if (type != null)
            {
                query = query.Where(x => x.Type == type.Value);
            }

            if (category != null)
            {
                query = query.Where(x => x.Category == category.Value);
            }
            return query;
        }
    }
}
