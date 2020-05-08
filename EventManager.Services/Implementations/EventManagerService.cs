using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using EventManager.Domain;
using EventManager.Repository;
using EventManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using EventManager.Services.ViewModels;

namespace EventManager.Services.Implementations
{
    public class EventManagerService : IEventManagerService
    {

        private readonly ILogger<EventManagerService> _logger;
        private readonly IEventManagerRepository _repository;
        private readonly IMapper _mapper;

        public EventManagerService(
            ILogger<EventManagerService> logger,
            IEventManagerRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EventViewModel> Create(EventViewModel model)
        {
            try
            {
                var mappedModel = _mapper.Map<Event>(model);
                _repository.Add(mappedModel);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<EventViewModel>(model);
                }
                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var existingEvent = await GetEvent(id);
                _repository.Delete(existingEvent);
                return await _repository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IEnumerable<EventViewModel>> Get()
        {
            try
            {
                var events = await _repository.GetAllEventsAsync(true);
                var result = _mapper.Map<IEnumerable<EventViewModel>>(events);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<EventViewModel> GetById(int id)
        {
            return _mapper.Map<EventViewModel>(await GetEvent(id, true));
        }

        public async Task<IEnumerable<EventViewModel>> Search(string query)
        {
            try
            {
                var events = await _repository.SearchEvents(query, true);
                var result = _mapper.Map<IEnumerable<EventViewModel>>(events);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<EventViewModel> Update(int id, EventViewModel model)
        {
            try
            {
                var existingEvent = await GetEvent(id);
                existingEvent = _mapper.Map(model, existingEvent);
                var lotsIds = new List<int>();
                var socialNetworksIds = new List<int>();
                model.Lots.ForEach(x => lotsIds.Add(x.Id));
                model.SocialNetworks.ForEach(x => socialNetworksIds.Add(x.Id));
                var lotsToRemove = existingEvent.Lots.Where(x => !lotsIds.Contains(x.Id));
                var socialNetworkIdsToRemove = existingEvent.Lots.Where(x => !socialNetworksIds.Contains(x.Id));
                if (lotsToRemove.Count() > 0) { _repository.DeleteRange(lotsToRemove.ToList()); }
                if (socialNetworkIdsToRemove.Count() > 0) { _repository.DeleteRange(socialNetworkIdsToRemove.ToList()); }
                _repository.Update(existingEvent);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<EventViewModel>(existingEvent);
                }
                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private async Task<Event> GetEvent(int eventId, bool includeGuests = false)
        {
            var existingEvent = await _repository.GetEventAsyncById(eventId, includeGuests);
            if (existingEvent == null)
            {
                _logger.LogWarning($"Event {eventId} does not exist");
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return existingEvent;
        }
    }
}
