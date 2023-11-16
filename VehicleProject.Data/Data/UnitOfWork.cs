using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Transactions;
using VehicleProject.Data.Interfaces;
using VehicleProject.Entity;
using VehicleProject.Entity.Models;

namespace VehicleProject.Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IDbContext _context;
        public UnitOfWork(IDbContext context) 
        {
            if(context == null) throw new ArgumentNullException("context");
            _context = context;
        }
        public IRepository<VehicleMake> vehicleMakeRepo => 
            new Repository<VehicleMake>(_context);
        public IRepository<VehicleModel> vehicleModelRepo =>
        new Repository<VehicleModel>(_context);

        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }
        public Task<int> AddAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    _context.Set<T>().Add(entity);
                }
                
                return Task.FromResult(1);

            }catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public Task<int> DeleteAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _context.Set<T>().Attach(entity);
                    _context.Set<T>().Remove(entity);
                }
                return Task.FromResult(1);
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public Task<int> DeleteAsync<T>(long id) where T : BaseEntity
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            return DeleteAsync<T>(entity);

        }


        public Task<int> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                var findEntity = _context.Set<T>().Find(entity.Id);
                
                if (findEntity == null)
                {
                    _context.Set<T>().Attach(entity);
                }else
                {
                    _context.Entry(findEntity).State = EntityState.Detached;
                    _context.Set<T>().Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }
                return Task.FromResult(1);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

 
    }
}
