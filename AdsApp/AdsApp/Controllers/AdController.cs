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

        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetAsync(int id)
        {
            return await _adService.GetAdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(Ad ad)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = await _adService.CreateAdAsync(ad);

            return Ok(id);
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
    }
}
