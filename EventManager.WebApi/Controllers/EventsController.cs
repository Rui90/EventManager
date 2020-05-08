using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Linq;
using EventManager.Services.Interfaces;
using EventManager.Services.ViewModels;

namespace EventManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController: ControllerBase
    {
        private readonly IEventManagerService _eventManagerService;

        public EventsController(IEventManagerService eventManagerService)
        {
            _eventManagerService = eventManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            var result = await _eventManagerService.Create(model);
            if (result != default) {
                return Created($"/api/event/{model.Id}", result);
            } 
            return BadRequest();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Update(int eventId, EventViewModel model)
        {
            var result = await _eventManagerService.Update(eventId, model);
            if (result != default)
            {
                return Created($"/api/event/{model.Id}", result);
            }
            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(int eventId)
        {
            var result = await _eventManagerService.Delete(eventId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _eventManagerService.Get());
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0) {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", "")).Trim();

                    using (var stream = new FileStream(fullPath, FileMode.Create)) {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            } catch (Exception e) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file {e.Message}");
            }
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById(int eventId)
        {
            return Ok(await _eventManagerService.GetById(eventId));
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            return Ok(await _eventManagerService.Search(search));
        }
    }
}