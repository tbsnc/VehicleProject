using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VehicleProject.Model;

namespace VehicleProject.Common
{
    public interface IDbContext
    {

        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
        int SaveChanges();
        DbEntityEntry Entry(object entity);

    }
}
