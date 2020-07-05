using System.Collections.Generic;
using System.Threading.Tasks;
using Arnoosha.Core.Entities;

namespace Arnoosha.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}
