using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleProject.Model;



namespace VehicleProject.Repository.Common
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task<int> AddAsync<T>(T entity) where T : BaseEntity;
        Task<int> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(long id) where T : BaseEntity;

        Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity;
        
        Task<T> GetById<T>(long id) where T : BaseEntity;

        Task<IEnumerable<T>> QueryStringFilter<T>(string s, string orderby, int per_page, int num_page) where T : BaseEntity;
    }

}

