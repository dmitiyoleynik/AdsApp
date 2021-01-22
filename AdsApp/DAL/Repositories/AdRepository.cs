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

        public async Task CreateAsync(Models.Ad ad)
        {
            ad.Updated = DateTime.Now;
            _context.Ads.Add(ad);

            await _context.SaveChangesAsync();
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

            return ad;
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
            oldAd.Tags = ad.Tags;

            _context.Ads.Update(oldAd);
            await _context.SaveChangesAsync();

            return oldAd;
        }

        private IQueryable<Models.Ad> AddFilters(IQueryable<Models.Ad> query, AdType? type, AdCategory? category)
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
