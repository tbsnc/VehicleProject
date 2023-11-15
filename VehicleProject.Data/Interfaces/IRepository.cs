using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Entity;

namespace VehicleProject.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(long id);
      //  void Insert(T entity);

        Task<IEnumerable<T>> GetAll();
    }
} 
