using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vehicle.Service;
using VehicleProject.Data.Interfaces;
using VehicleProject.Entity.Models;
using VehicleProject.WEB.Models;

namespace VehicleProject.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

       private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
     
        }

        public async Task<IActionResult> Index()
        {
            VehicleMake vehicleMake = new VehicleMake() { 
            Name="test",Abrv="test"};

            await _unitOfWork.AddAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
   
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