using AutoMapper;
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
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
     
        }

        public async Task<IActionResult> Index()
        {
            ////VehicleMake vehicleMake = new VehicleMake() { 
            ////Name="test",Abrv="test"};

            ////await _unitOfWork.AddAsync(vehicleMake);
            ////await _unitOfWork.CommitAsync();
            //var vehicleMake = await _unitOfWork.vehicleMakeRepo.GetAll();
            //var vehicleMakeDto = _mapper.Map<IEnumerable<VehicleMake>>(vehicleMake);

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