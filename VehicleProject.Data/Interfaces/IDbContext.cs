using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VehicleProject.Entity;
using System.Data.Entity.Infrastructure;

namespace VehicleProject.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
        int SaveChanges();
        DbEntityEntry Entry(object entity);

    }
}
