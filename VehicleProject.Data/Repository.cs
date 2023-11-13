using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Entity;

namespace VehicleProject.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;
        
        public Repository(IDbContext context)
        {
            this._context = context;
        }


        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();  
                }
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }



        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var errors in dbEx.EntityValidationErrors)
                {
                    foreach (var error in errors.ValidationErrors)
                    {
                        msg += $"Property: {error.PropertyName} Error: {error.ErrorMessage}\n";
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this._context.SaveChanges();
            }
            catch(DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var errors in dbEx.EntityValidationErrors)
                {
                    foreach (var error in errors.ValidationErrors)
                    {
                        msg += $"Property: {error.PropertyName} Error: {error.ErrorMessage}\n";
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                
                this._context.SaveChanges();
            }catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var errors in dbEx.EntityValidationErrors)
                {
                    foreach (var error in errors.ValidationErrors)
                    {
                        msg += $"Property: {error.PropertyName} Error: {error.ErrorMessage}\n";
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
    }
}
