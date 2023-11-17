using System.Threading.Tasks;
using System.Linq;
using VehicleProject.Model;
using System.Collections.Generic;

namespace VehicleProject.Service.Common
{
    public interface IVehicleService
    {
        Task<int> CommitAsync();
        Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity;

        Task<T> GetById<T>(long id) where T : BaseEntity;

        Task<int> AddAsync<T>(T entity) where T : BaseEntity;
        Task<int> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(long id) where T : BaseEntity;


    }
}
