using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController: ControllerBase
    {

        private readonly ILogger<GuestsController> _logger;
        private readonly IProAgilRepository _repository;

        public GuestsController(ILogger<GuestsController> logger, IProAgilRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guest model)
        {
            try {
                _repository.Add(model);
                if(await _repository.SaveChangesAsync()) {
                    return Created($"/api/guest/{model.Id}", model);
                }
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
            return BadRequest();
        }

        [HttpPut("{guestId}")]
        public async Task<IActionResult> Update(int guestId, Guest model)
        {
            try {
                var ev = await _repository.GetGuestAsync(guestId, false);
                if (ev == null) {
                    return NotFound();
                }
                _repository.Update(model);
                if (await _repository.SaveChangesAsync()) {
                    return Created($"/api/event/{model.Id}", model);
                }
            } catch (Exception e) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
            return BadRequest();
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> Delete(int guestId)
        {
            try {
                var ev = await _repository.GetGuestAsync(guestId, false);
                if (ev == null) {
                    return NotFound();
                }
                _repository.Delete(ev);
                if (await _repository.SaveChangesAsync()) {
                    return Ok();
                }
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var result = await _repository.GetAllGuestsAsync(true);
                return Ok(result);
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetById(int guestId)
        {
            try {
                var result = await _repository.GetGuestAsync(guestId, true);
                return Ok(result);
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetByTheme(string name)
        {
            try {
                var result = await _repository.GetAllGuestsByName(name, true);
                return Ok(result);
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }
    }

}