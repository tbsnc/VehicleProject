using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.InteropServices;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;
using VehicleProject.Service.Common;
using VehicleProject.WebAPI.Controllers;

namespace VehicleProject.UnitTest.VehicleControllerAPITests
{
    public class VehicleModelAPITest
    {
        private readonly Mock<IVehicleService> _vehicleServiceMoq;
        private readonly IMapper _mapperMoq;
        private readonly VehicleModelAPIController _controller;
        public VehicleModelAPITest()
        {
            _vehicleServiceMoq = new Mock<IVehicleService>();

            var mapperCfg = new MapperConfiguration(mc =>
            mc.AddProfile(new VehicleProject.Common.Helper.AutoMapperProfiles())
            );

            IMapper mapper = mapperCfg.CreateMapper();
            _mapperMoq = mapper;
            _controller = new VehicleModelAPIController(_vehicleServiceMoq.Object, _mapperMoq);
        }


        [Fact]
        public async Task VehicleModelAPIController_GetAllVehicleModel_Success()
        {
            //arrange
            
            _vehicleServiceMoq.Setup(x => x.GetAll<VehicleModel>()).ReturnsAsync(new List<VehicleModel>(){ new VehicleModel() { Name = "Test",Abrv = "Test"} });
            //act
            var result = await _controller.GetAllVehicleModel();

            //assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task VehicleModelAPIController_GetAllVehicleModel_Fail()
        {
            //################## GetAll returns empty list ##############
            //arrange
            _vehicleServiceMoq.Setup(x => x.GetAll<VehicleModel>()).ReturnsAsync(value: new List<VehicleModel>());

            //act
            var result = await _controller.GetAllVehicleModel();
            ObjectResult? objResult = result as ObjectResult;
            //assert
            objResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);


            //################## GetAll returns null ##############
            //arrange
            _vehicleServiceMoq.Setup(x => x.GetAll<VehicleModel>()).ReturnsAsync(value: null);

            //act
            result = await _controller.GetAllVehicleModel();
            objResult = result as ObjectResult;
            //assert
            objResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task VehicleModelAPIController_AddModel_Success()
        {
            //arrange
            VehicleModelDTO vehicleModelDto= new VehicleModelDTO() { Name = "Test",Abrv = "Test" };

            //act
            var result = await _controller.AddModel(vehicleModelDto);
            ObjectResult? objectResult = result as ObjectResult;

            //assert
            objectResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task VehicleModelAPIController_AddModel_Fail()
        {
            //arrange
            VehicleModelDTO vehicleModelDtoInvalid = new VehicleModelDTO() { Name = "Test",Abrv=""};

            //act
            var resultInvalid = await _controller.AddModel(vehicleModelDtoInvalid);
            var resultNull = await _controller.AddModel(null);
            ObjectResult? objectResultInvalid = resultInvalid as ObjectResult;
            ObjectResult? objectResultNull = resultNull as ObjectResult;


            //assert
            objectResultInvalid.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objectResultNull.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }

        [Fact]
        public async Task VehicleModelAPIController_UpdateModel_Success()
        {
            //arrange
            int id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleModel>(id)).ReturnsAsync(new VehicleModel() { Name = "Test", Abrv = "Test" });

            //act
            var result =await _controller.UpdateModel(id, new VehicleModelDTO() { Name = "Test", Abrv = "Test"});

            //assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task VehicleModelAPIController_UpdateModel_Fail()
        {
            //############## bad request id = 0 ##################
            //arrange 
            int id = 0;

            //act
            var result = await _controller.UpdateModel(id, new VehicleModelDTO());

            //assert
            result.Should().BeOfType<BadRequestResult>();

            //############## vehiclemodel not found ###############
            //arrange 
            id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleModel>(id)).ReturnsAsync(value: null);

            //act
            result = await _controller.UpdateModel(id, new VehicleModelDTO() { Name = "Test", Abrv = "Test" });
            ObjectResult? objectResult = result as ObjectResult;

            //assert
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);


        }

        [Fact]
        public async Task VehicleModelAPIController_DeleteModel_Success()
        {
            //arrange
            int id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleModel>(id)).ReturnsAsync(new VehicleModel() { Name ="Test",Abrv = "Test"});

            //act
            var result = await _controller.DeleteModel(id);

            //assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task VehicleModelAPIController_DeleteModel_Fail()
        {
            //########## bad request ###############
            //arrange 
            int id = 0;

            //act
            var result = await _controller.DeleteModel(id);

            //assert
            result.Should().BeOfType<BadRequestResult>();

            //########## vehicle model not found ###############
            //arrange 
            id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleModel>(id)).ReturnsAsync(value : null);
            
            //act
            result = await _controller.DeleteModel(id);
            ObjectResult? objectResult = result as ObjectResult;

            //assert
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        }
    }
}
