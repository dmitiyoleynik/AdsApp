using BL.DTO;
using BL.Services;
using DAL.Exceptions;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BL;

namespace AdsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        /// <summary>
        /// Request ad with filtration
        /// </summary>
        /// <remarks>
        /// 
        /// Could be requested with type and category, in that case will return first suitable ad and next token
        /// If request with token, will return next sutable ad and new token. If token is indicated, type and category will be ignored.
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<AdResponse>> Get([FromQuery] string token,
            AdType? type, AdCategory? category)
        {
            try
            {
                ActionResult actionResult;

                if (token != null)
                {
                    var response = await _adService.GetAdByTokenAsync(token);
                    actionResult = Ok(response);
                }
                else
                {
                    var response = await _adService.GetAdWithNoTokenAsync(type, category);
                    actionResult = Ok(response);
                }

                return actionResult;
            }
            catch (InvalidNextTokenException)
            {
                return BadRequest(Constants.Messages.InvalidToken);
            }
            catch (RecordNotFoundException)
            {
                ActionResult actionResult;

                if (token != null)
                {
                    actionResult = Ok(Constants.Messages.QueueFinished);
                }
                else
                {
                    actionResult = BadRequest();
                }

                return actionResult;
            }

        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(Ad ad)
        {
            await _adService.CreateAdAsync(ad);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Ad>> PutAsync(Ad ad)
        {
            try
            {
                var newAd = await _adService.UpdateAdAsync(ad);

                return Ok(newAd);
            }
            catch (RecordNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _adService.DeleteAdAsync(id);

                return NoContent();
            }
            catch (RecordNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpGet("statistics")]
        public async Task<Statistics> Statistics()
        {
            return await _adService.GetStatistics();
        }
    }
}
