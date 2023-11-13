using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleProject.Entity
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Abrv { get; set; }

    }
}
