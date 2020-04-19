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
    public class EventsController: ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly IProAgilRepository _repository;

        public EventsController(ILogger<EventsController> logger, IProAgilRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event model)
        {
            try {
                _repository.Add(model);
                if(await _repository.SaveChangesAsync()) {
                    return Created($"/api/event/{model.Id}", model);
                }
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
            return BadRequest();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Update(int eventId, Event model)
        {
            try {
                var ev = await _repository.GetEventAsyncById(eventId, false);
                if (ev == null) {
                    return NotFound();
                }
                _repository.Update(model);
                if (await _repository.SaveChangesAsync()) {
                    return Created($"/api/event/{model.Id}", model);
                }
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(int eventId)
        {
            try {
                var ev = await _repository.GetEventAsyncById(eventId, false);
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
                var result = await _repository.GetAllEventsAsync(true);
                return Ok(result);
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById(int eventId)
        {
            try {
                var result = await _repository.GetEventAsyncById(eventId, true);
                return Ok(result);
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }

        [HttpGet("getByTheme/{theme}")]
        public async Task<IActionResult> GetByTheme(string theme)
        {
            try {
                var result = await _repository.GetAllEventsAsyncByTheme(theme, true);
                return Ok(result);
            } catch (Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failed");
            }
        }
    }
}