using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VehicleProject.Entity.Models
{
    public class VehicleModel : BaseEntity
    {
        public long MakeId { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }


    }
}
