using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ProAgil.Repository;
using ProAgil.Domain;
using AutoMapper;
using ProAgil.WebApi.ViewModels;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ProAgil.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController: ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly IProAgilRepository _repository;
        private readonly IMapper _mapper;

        public EventsController(
            ILogger<EventsController> logger, 
            IProAgilRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            try {
                var mappedModel = _mapper.Map<Event>(model);
                _repository.Add(mappedModel);
                if(await _repository.SaveChangesAsync()) {
                    return Created($"/api/event/{model.Id}", _mapper.Map<EventViewModel>(mappedModel));
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
                ev = _mapper.Map(model, ev);
                _repository.Update(ev);
                if (await _repository.SaveChangesAsync()) {
                    return Created($"/api/event/{model.Id}",  _mapper.Map<EventViewModel>(ev));
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
                var events = await _repository.GetAllEventsAsync(true);
                var result = _mapper.Map<IEnumerable<EventViewModel>>(events);
                return Ok(result);
            } catch (Exception e) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failed {e.Message} mapper {_mapper}");
            }
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById(int eventId)
        {
            try {
                var result = _mapper.Map<EventViewModel>(await _repository.GetEventAsyncById(eventId, true));
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