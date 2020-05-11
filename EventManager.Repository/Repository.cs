using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventManager.Domain;
using EventManager.Repository.Data;

namespace EventManager.Repository
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context) {
            this._context = context;
            this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeGuests = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Lots).Include(e => e.SocialNetworks);
            if (includeGuests) {
                query = query.Include(e => e.GuestsEvents).ThenInclude(e => e.Guest);
            }
            query = query.OrderByDescending(q => q.Date);
            return await query.ToArrayAsync();
        }

        public async Task<Event[]> SearchEvents(string searchQuery, bool includeGuests = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Lots).Include(e => e.SocialNetworks);
            if (includeGuests) {
                query = query.Include(e => e.GuestsEvents).ThenInclude(e => e.Guest);
            }
            query = query.OrderByDescending(q => q.Date).Where(e => e.Theme.ToLower().Contains(searchQuery.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventAsyncById(int EventId, bool includeGuests = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Lots).Include(e => e.SocialNetworks);
            if (includeGuests) {
                query = query.Include(e => e.GuestsEvents).ThenInclude(e => e.Guest);
            }
            query = query.Where(e => e.Id == EventId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Guest[]> GetAllGuestsAsync(bool includeEvents)
        {
            IQueryable<Guest> query = _context.Guests.Include(e => e.SocialNetworks);
            if (includeEvents) {
                query = query.Include(e => e.GuestsEvents).ThenInclude(e => e.Event);
            }
            query = query.OrderBy(q => q.Name);
            return await query.ToArrayAsync();
        }

        public async Task<Guest> GetGuestAsync(int guestId, bool includeEvents)
        {
            IQueryable<Guest> query = _context.Guests.Include(e => e.SocialNetworks);
            if (includeEvents) {
                query = query.Include(e => e.GuestsEvents).ThenInclude(e => e.Event);
            }
            query = query.Where(q => q.Id == guestId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Guest[]> SearchGuests(string search, bool includeEvents)
        {
            IQueryable<Guest> query = _context.Guests.Include(e => e.SocialNetworks);
            if (includeEvents) {
                query = query.Include(e => e.GuestsEvents).ThenInclude(e => e.Event);
            }
            query = query.OrderBy(q => q.Name).Where(q => q.Name.ToLower().Contains(search.ToLower()));
            return await query.ToArrayAsync();
        }

        public void DeleteRange<T>(IList<T> entity) where T : class
        {
            _context.RemoveRange(entity);
        }
    }
}