using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Entity.Models;

namespace Vehicle.Service
{
    public interface IVehicleService
    {
        IQueryable<VehicleMake> GetAll();
        VehicleMake GetVehicle(long id);

        void InsertVehicle(VehicleMake vehicle);
        void UpdateVehicle(VehicleMake vehicle);
        void DeleteVehicle(VehicleMake vehicle);

    }
}
