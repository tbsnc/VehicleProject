using System.Data.Entity.ModelConfiguration;
using VehicleProject.Model;

namespace VehicleProject.Data.Mapping
{
    public class VehicleModelMap : EntityTypeConfiguration<VehicleModel>
    {
        public VehicleModelMap() 
        {
            HasKey(t  => t.Id);
            Property(t => t.Name);
            Property(t => t.Abrv);
            HasRequired(t => t.VehicleMake).WithMany().HasForeignKey(u => u.MakeId);
            
            ToTable("VehicleModel");

        }

    }
}
