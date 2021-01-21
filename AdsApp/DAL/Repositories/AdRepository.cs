using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Models.Ad> GetAsync(AdType? type, AdCategory? category)
        {
            var query = AddFilters(_context.Ads.Where(x => x.Deleted == null), type, category);
            var ad = await query.FirstAsync(x=>x.IsActive);

            return new Models.Ad { Id = ad.Id, Category = ad.Category, Type = ad.Type, Cost = ad.Cost, Content = ad.Content, IsActive = ad.IsActive };
        }

        public async Task<Models.Ad> GetNextAsync(int lastShownAdId, AdType? type, AdCategory? category)
        {

            var query = AddFilters(_context.Ads.Where(x => x.Deleted == null), type, category);
            var ad = await query.FirstAsync(x => x.IsActive && x.Id>lastShownAdId);

            return new Models.Ad { Id=ad.Id, Type = ad.Type, Category = ad.Category, Cost = ad.Cost, Content=ad.Content, IsActive = ad.IsActive };
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

        private IQueryable<EFModels.Ad> AddFilters( IQueryable<EFModels.Ad> query,AdType? type, AdCategory? category)
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
