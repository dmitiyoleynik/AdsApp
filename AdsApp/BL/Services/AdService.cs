using BL.DTO;
using BL.Models;
using DAL.Exceptions;
using DAL.Models;
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
        private readonly INextTokenService _nextTokenService;

        public AdService(IAdRepository adRepository,
            INextTokenService nextTokenService)
        {
            _adRepository = adRepository;
            _nextTokenService = nextTokenService;
        }

        public async Task<int> CreateAdAsync(Ad ad)
        {
            return await _adRepository.CreateAsync(ad);
        }

        public async Task<Ad> GetAdAsync(int id)
        {
            return await _adRepository.GetAsync(id);
        }

        public async Task DeleteAdAsync(int id)
        {
            try
            {
                await _adRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new RecordNotFoundException();
            }
        }

        public async Task<Ad> UpdateAdAsync(Ad ad)
        {
            try
            {
                return await _adRepository.UpdateAsync(ad);
            }
            catch (Exception)
            {
                throw new RecordNotFoundException();
            }
        }

        public async Task<AdResponse> GetAdByTokenAsync(string encodedToken)
        {
            try
            {
                var response = new AdResponse();
                var token = _nextTokenService.Decode(encodedToken);
                var ad = await _adRepository.GetNextAsync(token.LastShownAdId);

                token.LastShownAdId = ad.Id;
                response.Ad = ad;
                response.Token = _nextTokenService.Encode(token);

                return response;

            }
            catch (Exception)
            {
                throw new RecordNotFoundException();
            }
        }

        public async Task<AdResponse> GetAdWithNoTokenAsync()
        {
            try
            {
                var adResponse = new AdResponse();
                var ad = await _adRepository.GetAsync();
                var token = new NextToken();

                token.LastShownAdId = ad.Id;
                adResponse.Ad = ad;
                adResponse.Token = _nextTokenService.Encode(token);

                return adResponse;
            }
            catch (Exception)
            {
                throw new RecordNotFoundException();
            }
        }
    }
}
