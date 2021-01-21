using BL.DTO;
using BL.Services;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<AdResponse>> Get([FromQuery] string token,
            AdType? type, AdCategory? category)
        {
            ActionResult actionResult;

            try
            {
                if (token!=null)
                {
                    var response = await _adService.GetAdByTokenAsync(token);
                    actionResult = Ok(response);
                }
                else
                {
                    var response = await _adService.GetAdWithNoTokenAsync(type,category);
                    actionResult = Ok(response);
                }
            }
            catch (InvalidNextTokenException)
            {
                actionResult = BadRequest();
            }
            catch (RecordNotFoundException)
            {
                if (token != null)
                {
                    actionResult = Ok("Queue is finished.");
                }
                else
                {
                    actionResult = BadRequest();
                }
            }

            return actionResult;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(Ad ad)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _adService.CreateAdAsync(ad);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Ad>> PutAsync(Ad ad)
        {
            try
            {
                var id = await _adService.UpdateAdAsync(ad);
             
                return Ok(id);
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
            }
            catch (RecordNotFoundException)
            {
                return BadRequest();
            }

            return NoContent();
        }
      
        [HttpGet("statistics")]
        public async Task<Statistics> Statistics()
        {
            return await _adService.GetStatistics();
        }
    }
}
