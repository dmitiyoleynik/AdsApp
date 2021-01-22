using BL.DTO;
using BL.Models;
using DAL.Exceptions;
using DAL.Models;
using DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly INextTokenService _nextTokenService;

        public AdService(IAdRepository adRepository,
            INextTokenService nextTokenService)
        {
            _adRepository = adRepository ?? throw new ArgumentNullException(nameof(adRepository));
            _nextTokenService = nextTokenService ?? throw new ArgumentNullException(nameof(nextTokenService));

            TypeAdapterConfig<string, Tag>.NewConfig()
                .MapWith(str => new Tag { Value = str });
            TypeAdapterConfig<Tag, string>.NewConfig()
                .MapWith(tag => tag.Value);
        }

        public async Task CreateAdAsync(Models.Ad ad)
        {
            var dalAd = ad.Adapt<DAL.Models.Ad>();
            await _adRepository.CreateAsync(dalAd);
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

        public async Task<Models.Ad> UpdateAdAsync(Models.Ad ad)
        {
            try
            {
                var oldAd = ad.Adapt<DAL.Models.Ad>();
                var newAd = await _adRepository.UpdateAsync(oldAd);

                return newAd.Adapt<Models.Ad>();
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
                var ad = await _adRepository.GetNextAsync(token.Type, token.Category, token.LastShownAdId);

                token.LastShownAdId = ad.Id;
                response.Ad = ad.Adapt<Models.Ad>();
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
                var ad = await _adRepository.GetNextAsync(type, category);
                var token = new NextToken();

                token.Category = category;
                token.Type = type;
                token.LastShownAdId = ad.Id;
                adResponse.Ad = ad.Adapt<Models.Ad>();
                adResponse.Token = _nextTokenService.Encode(token);

                return adResponse;
            }
            catch (Exception)
            {
                throw new RecordNotFoundException();
            }
        }

        public async Task<Statistics> GetStatistics()
        {
            var statistics = new Statistics();
            var ads = await _adRepository.TopAdsAsync(10);

            statistics.TopCategories = await _adRepository.TopCategoriesAsync(3);
            statistics.TopAds = ads.Adapt<List<Models.Ad>>();
            statistics.RequestsPerType = await _adRepository.GetViewsPerTypeAsync();

            return statistics;
        }
    }
}
