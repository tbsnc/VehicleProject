﻿using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Diagnostics;
using Vehicle.Service;
using VehicleProject.Data;
using VehicleProject.Entity;
using VehicleProjectWeb.Models;

namespace VehicleProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<BaseEntity> _repository;
        private readonly IVehicleService _vehicleService;
        public HomeController(ILogger<HomeController> logger, IRepository<BaseEntity> repository, IVehicleService vehicleService)
        {
            _logger = logger;
            _repository = repository;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
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