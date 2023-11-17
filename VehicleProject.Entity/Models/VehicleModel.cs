namespace VehicleProject.Model
{
    public class VehicleModel : BaseEntity
    {
 
        public long MakeId { get; set; }


        public virtual VehicleMake VehicleMake { get; set; }

    }
}
