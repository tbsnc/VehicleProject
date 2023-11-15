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
            var vehicleMake = _vehicleService.GetAllVehicleMake();


            if (vehicleMake == null)
            {
                return NotFound();
            }
      
            return Ok(vehicleMake);
           
        }
    }
}
