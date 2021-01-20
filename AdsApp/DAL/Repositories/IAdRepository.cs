using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAdRepository
    {
        Task<int> CreateAsync(Models.Ad ad);
        Task<Ad> GetAsync(int id);
        Task<Ad> UpdateAsync(Models.Ad ad);
        Task<bool> DeleteAsync(int id);
    }
}
