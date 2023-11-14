using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Service;
using VehicleProject.Entity.Models;

namespace VehicleProject.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAPIController : ControllerBase
    {
        private IVehicleService _vehicleService;

        public  VehicleAPIController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMake>>> GetVehicleMake()
        {
            List<VehicleMake> vehicleMake = _vehicleService.GetAll().ToList();
            
            return _vehicleService.GetAll().ToList();
        }
    }
}
