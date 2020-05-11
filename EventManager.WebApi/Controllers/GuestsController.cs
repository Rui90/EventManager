using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using EventManager.Repository;
using EventManager.Domain;
using EventManager.Services.Interfaces;
using EventManager.Services.ViewModels;

namespace EventManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController: ControllerBase
    {

        private readonly ILogger<GuestsController> _logger;
        private readonly IGuestService _guestService;

        public GuestsController(ILogger<GuestsController> logger, IGuestService guestService)
        {
            _logger = logger;
            _guestService = guestService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuestViewModel model)
        {
            var result = await _guestService.Create(model);
            if (result != default)
            {
                return Created($"/api/event/{model.Id}", result);
            }
            return BadRequest();
        }

        [HttpPut("{guestId}")]
        public async Task<IActionResult> Update(int guestId, GuestViewModel model)
        {
            var result = await _guestService.Update(guestId, model);
            if (result != default)
            {
                return Created($"/api/event/{model.Id}", result);
            }
            return BadRequest(); ;
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> Delete(int guestId)
        {
            var result = await _guestService.Delete(guestId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _guestService.Get());
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetById(int guestId)
        {
            return Ok(await _guestService.GetById(guestId));
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            return Ok(await _guestService.Search(search));
        }
    }

}