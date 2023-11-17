using System.Data.Entity.ModelConfiguration;
using VehicleProject.Model;

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
