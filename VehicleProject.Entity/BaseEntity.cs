using VehicleProject.Model.Common;

namespace VehicleProject.Model
{
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Abrv { get; set; }
        long IEntity.Id
        {
            get { return Id; }
            set { Id = value; } 
        }
    }
}
