using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        // ENTITY CREATE, UPDATE, DELETE
         void Add<T>(T entity) where T : class;

         void Update<T> (T entity) where T : class;

         void Delete<T> (T entity) where T : class;

         Task<bool> SaveChangesAsync();

        // LIST GETTERS
         Task<Event[]> GetAllEventsAsyncByTheme(string Theme, bool includeGuests);

         Task<Event[]> GetAllEventsAsync(bool includeGuests);

         Task<Event> GetEventAsyncById(int EventId,  bool includeGuests);

         Task<Guest[]> GetAllGuestsAsync(bool includeEvents);

         Task<Guest> GetGuestAsync(int guestId, bool includeEvents);

         Task<Guest[]> GetAllGuestsByName(string name, bool includeEvents);

    }
}