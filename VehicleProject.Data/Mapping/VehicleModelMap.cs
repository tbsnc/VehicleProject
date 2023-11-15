using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Entity.Models;

namespace VehicleProject.Data.Mapping
{
    public class VehicleModelMap : EntityTypeConfiguration<VehicleModel>
    {
        public VehicleModelMap() 
        {
            HasKey(t  => t.Id);
            Property(t => t.Name);
            Property(t => t.Abrv);
            //HasRequired(t => t.VehicleMake).WithRequiredDependent(u => u.VehicleModel);
            HasRequired(t => t.VehicleMake).WithMany().HasForeignKey(u => u.MakeId);
            
            ToTable("VehicleModel");

        }

    }
}
