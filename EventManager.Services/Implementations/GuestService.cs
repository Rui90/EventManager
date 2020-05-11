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
    public class GuestService : IGuestService
    {
        private readonly ILogger<GuestService> _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GuestService(
            ILogger<GuestService> logger,
            IRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GuestViewModel> Create(GuestViewModel model)
        {
            try
            {
                var mappedModel = _mapper.Map<Guest>(model);
                _repository.Add(mappedModel);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<GuestViewModel>(model);
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
                var guest = await GetGuest(id);
                _repository.Delete(guest);
                return await _repository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IEnumerable<GuestViewModel>> Get()
        {
            try
            {
                var events = await _repository.GetAllGuestsAsync(true);
                var result = _mapper.Map<IEnumerable<GuestViewModel>>(events);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GuestViewModel> GetById(int id)
        {
            return _mapper.Map<GuestViewModel>(await GetGuest(id, true));
        }

        public async Task<IEnumerable<GuestViewModel>> Search(string query)
        {
            try
            {
                var guests = await _repository.SearchGuests(query, true);
                var result = _mapper.Map<IEnumerable<GuestViewModel>>(guests);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GuestViewModel> Update(int id, GuestViewModel model)
        {
            try
            {
                var guest = await GetGuest(id);
                _repository.Update(model);
                if (await _repository.SaveChangesAsync())
                {
                    return model;
                }
                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private async Task<Guest> GetGuest(int guestId, bool includeGuests = false)
        {
            var guest = await _repository.GetGuestAsync(guestId, includeGuests);
            if (guest == null)
            {
                _logger.LogWarning($"Guest {guestId} does not exist");
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return guest;
        }

    }
}
