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

        public async Task<bool> DeleteAsync(int id)
        {
            bool isDeleted = false;

            var ad = await _context.Ads.FirstOrDefaultAsync(x => x.Id == id);

            if (ad != null && ad.Deleted == null)
            {
                ad.Deleted = DateTime.Now;
                ad.Updated = DateTime.Now;
                _context.Ads.Update(ad);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }

            return isDeleted;
        }

        public async Task<Models.Ad> GetAsync(int id)
        {
            var ad = await _context.Ads.FirstOrDefaultAsync(x => x.Id == id);
            return new Models.Ad { Id = ad.Id, Category = ad.Category, Type = ad.Type, Cost = ad.Cost, Content = ad.Content, IsActive = ad.IsActive };
        }

        public async Task<Models.Ad> UpdateAsync(Models.Ad ad)
        {
            var oldAd = await _context.Ads.FirstOrDefaultAsync(x => x.Id == ad.Id);

            _context.Ads.Update(new EFModels.Ad
            {
                Id = ad.Id,
                Category = ad.Category,
                Type = ad.Type,
                Cost = ad.Cost,
                Content = ad.Content,
                IsActive = ad.IsActive,
                Updated = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return ad;
        }
    }
}
