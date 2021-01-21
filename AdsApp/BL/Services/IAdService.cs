using BL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAdService
    {
        Task<int> CreateAdAsync(Ad ad);
        Task<AdResponse> GetAdByTokenAsync(string token);
        Task<AdResponse> GetAdWithNoTokenAsync(AdType? type, AdCategory? category);
        Task DeleteAdAsync(int id);
        Task<Ad> UpdateAdAsync(Ad ad);
    }
}
