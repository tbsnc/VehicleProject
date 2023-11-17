using System.Linq;
using System.Threading.Tasks;
using VehicleProject.Service.Common;
using VehicleProject.Model;
using VehicleProject.Repository.Common;
using System.Collections.Generic;

namespace VehicleProject.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public async Task<int> CommitAsync()
        {
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> AddAsync<T>(T entity) where T : BaseEntity
        {
            return await _unitOfWork.AddAsync(entity);
        }

        public async Task<int> DeleteAsync<T>(T entity) where T : BaseEntity
        {
            return await _unitOfWork.DeleteAsync(entity);
        }

        public async Task<int> DeleteAsync<T>(long id) where T : BaseEntity
        {
            return await _unitOfWork.DeleteAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity
        {
            return await _unitOfWork.GetAll<T>();
        }

        public async Task<T> GetById<T>(long id) where T : BaseEntity
        {
            return await _unitOfWork.GetById<T>(id);
        }

        public async Task<int> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            return await _unitOfWork.UpdateAsync<T>(entity);
        }
    }
}
