using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace VehicleProject.Entity.Models
{
    public class VehicleMake : BaseEntity
    {
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
