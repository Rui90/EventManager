using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Services.Interfaces
{
    public interface IBaseService<T>
    {
        public Task<T> Create(T model);

        public Task<T> Update(int id, T model);

        public Task<bool> Delete(int id);

        public Task<IEnumerable<T>> Get();

        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> Search(string query);
    }
}
