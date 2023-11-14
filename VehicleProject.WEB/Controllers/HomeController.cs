using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vehicle.Service;
using VehicleProject.Entity.Models;
using VehicleProject.WEB.Models;

namespace VehicleProject.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IVehicleService _vehicleService;
        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            VehicleMake vehicle = new VehicleMake() 
            {
                Abrv="Audi",
                Name ="Audi"
                
            };    
            _vehicleService.InsertVehicle(vehicle);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}