using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Entity;
using VehicleProject.Entity.Models;

namespace VehicleProject.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<VehicleMake> vehicleMakeRepo { get; }
        IRepository<VehicleModel> vehicleModelRepo { get; }

        Task<int> CommitAsync();
        Task<int> AddAsync<T>(T entity) where T : BaseEntity;
        Task<int> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(long id) where T : BaseEntity;
    }

}

