using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAdService
    {
        Task<int> CreateAdAsync(Ad ad);
        Task<Ad> GetAdAsync(int id);
        Task DeleteAdAsync(int id);
        Task<Ad> UpdateAdAsync(Ad ad);
    }
}
