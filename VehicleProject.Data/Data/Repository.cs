using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
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

 

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.Entities.ToListAsync();
        }



        public async Task<T> GetById(long id)
        {
            return await this.Entities.FirstAsync(x => x.Id == id);
        }

 

        
    }
}
