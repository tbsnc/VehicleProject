namespace VehicleProject.Model.Common
{
    public interface IEntity
    {
        long Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }
    }
}
