using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Data;
using VehicleProject.Entity.Models;

namespace Vehicle.Service
{
    public class VehicleService : IVehicleService
    {
        private IRepository<VehicleMake> vehicleMakeRepository;
        private IRepository<VehicleModel> vehicleModelRepository;

        public VehicleService(IRepository<VehicleMake> makeRepo, IRepository<VehicleModel> modelRepo)
        {
            vehicleMakeRepository = makeRepo;
            vehicleModelRepository = modelRepo;
        }

        public void DeleteVehicle(VehicleMake vehicle)
        {
            vehicleModelRepository.Delete(vehicle.VehicleModel);
            vehicleMakeRepository.Delete(vehicle);
        }

        public IQueryable<VehicleMake> GetAll()
        {
            return vehicleMakeRepository.Table;
        }

        public VehicleMake GetVehicle(long id)
        {
            return vehicleMakeRepository.GetById(id);
        }

        public void InsertVehicle(VehicleMake vehicle)
        {
            vehicleMakeRepository.Insert(vehicle);
        }

        public void UpdateVehicle(VehicleMake vehicle)
        {
            vehicleMakeRepository.Update(vehicle);  
        }
    }
}
