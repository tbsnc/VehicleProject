using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VehicleProject.Entity.Models;

namespace VehicleProject.Data.Mapping
{
    public class VehicleMakeMap : EntityTypeConfiguration<VehicleMake>
    {
        public VehicleMakeMap() 
        {
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired();
            Property(t => t.Abrv).IsRequired();
            ToTable("VehicleMake");
        }



    }
}
