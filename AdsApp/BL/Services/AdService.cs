using BL.DTO;
using BL.Models;
using DAL.Exceptions;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Text.Json;
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
                var ad = await _adRepository.GetNextAsync(token.LastShownAdId,token.Type, token.Category);

                token.LastShownAdId = ad.Id;
                response.Ad = ad;
                response.Token = _nextTokenService.Encode(token);

                return response;

            }
            catch (JsonException)
            {
                throw new InvalidNextTokenException();
            }
            catch (Exception)
            {
                throw new RecordNotFoundException();
            }
        }

        public async Task<AdResponse> GetAdWithNoTokenAsync(AdType? type, AdCategory? category)
        {
            try
            {
                var adResponse = new AdResponse();
                var ad = await _adRepository.GetAsync(type,category);
                var token = new NextToken();

                token.Category = category;
                token.Type = type;
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
