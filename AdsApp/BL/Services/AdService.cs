﻿using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AdService:IAdService
    {
        private readonly IAdRepository _adRepository;
        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public async Task<int> CreateAdAsync(Ad ad)
        {
            return await _adRepository.CreateAsync(ad);
        }

        public async Task<Ad> GetAdAsync(int id)
        {
            return await _adRepository.GetAsync(id);
        }

        public async Task<bool> DeleteAdAsync(int id)
        {
            return await _adRepository.DeleteAsync(id);
        }

        public async Task<Ad> UpdateAdAsync(Ad ad)
        {
            return await _adRepository.UpdateAsync(ad);
        }
    }
}