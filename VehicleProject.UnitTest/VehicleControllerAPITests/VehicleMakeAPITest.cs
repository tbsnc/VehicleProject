using Moq;
using FluentAssertions;
using VehicleProject.WebAPI.Controllers;
using VehicleProject.Service.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.Common.Helper;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;

using System.Transactions;
using VehicleProject.Service;
using Microsoft.AspNetCore.Http;

namespace VehicleProject.UnitTest.VehicleControllerAPITests
{
    public class VehicleMakeAPITest
    {
        private readonly Mock<IVehicleService> _vehicleServiceMoq;
        private readonly IMapper _mapperMoq;
        private readonly VehicleMakeAPIController _controller;
        public VehicleMakeAPITest()
        {
            _vehicleServiceMoq = new Mock<IVehicleService>();

            if (_mapperMoq == null)
            {
                var mappingCfg = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfiles());
                });
                IMapper mapper = mappingCfg.CreateMapper();
                _mapperMoq = mapper;

            }
            _controller = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);


        }


        [Fact]
        public async Task VehicleMakeAPIController_GetAll_Success()
        {
            //arrange
            _vehicleServiceMoq.Setup(x => x.GetAll<VehicleMake>()).ReturnsAsync(new List<VehicleMake>() { new VehicleMake() { Name = "Test", Abrv = "Test" } });

            //act
            var result = await _controller.GetAllVehicleMake();


            //assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task VehicleModelAPIController_GetAllVehicleMake_Fail()
        {
            //################## GetAll returns empty list ##############
            //arrange
            _vehicleServiceMoq.Setup(x => x.GetAll<VehicleMake>()).ReturnsAsync(value: new List<VehicleMake>());

            //act
            var result = await _controller.GetAllVehicleMake();
            ObjectResult? objResult = result as ObjectResult;
            //assert
            objResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);


            //################## GetAll returns null ##############
            //arrange
            _vehicleServiceMoq.Setup(x => x.GetAll<VehicleMake>()).ReturnsAsync(value: null);

            //act
            result = await _controller.GetAllVehicleMake();
            objResult = result as ObjectResult;
            //assert
            objResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
        [Fact]

        public async Task VehicleMakeAPIController_AddMake_Success()
        {
            //arrange
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);

            //act
            var result = await handler.AddMake(new VehicleMakeDTO() {Name = "Test",Abrv="Test" });
            ObjectResult? objectResult = result as ObjectResult;
          
            //assert
            objectResult.StatusCode.Should().Be(201);

        }

        [Fact]
        public async Task VehicleMakeAPIController_AddMake_Fail()
        {
            //arrange
            VehicleMakeDTO vehicleMakeDtoInvalid = new VehicleMakeDTO()
            {
                Name = string.Empty,
                Abrv = string.Empty
            };

            //act

            var resultNull = await _controller.AddMake(null);
            ObjectResult? objectResultNull = resultNull as ObjectResult;
            var resultInvalid = await _controller.AddMake(vehicleMakeDtoInvalid);
            ObjectResult? objectResultInvalid = resultInvalid as ObjectResult;

            //assert
            objectResultNull.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objectResultInvalid.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }


        [Fact]
        public async Task VehicleMakeAPIController_UpdateMake_Success()
        {
            //arrange
            int id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleMake>(id)).ReturnsAsync(new VehicleMake() { Name = "Test", Abrv = "Test" });

            //act
            var result = await _controller.UpdateMake(id, new VehicleMakeDTO() { Name = "Test", Abrv = "Test" });

            //assert
            result.Should().BeOfType<OkResult>();

        }

        [Fact]
        public async Task VehicleMakeAPIController_UpdateMake_Fail()
        {
            //############## bad request id = 0 ##################
            //arrange 
            int id = 0;

            //act
            var result = await _controller.UpdateMake(id, new VehicleMakeDTO());

            //assert
            result.Should().BeOfType<BadRequestResult>();

            //############## vehiclemodel not found ###############
            //arrange 
            id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleMake>(id)).ReturnsAsync(value: null);

            //act
            result = await _controller.UpdateMake(id, new VehicleMakeDTO() { Name = "Test", Abrv = "Test" });
            ObjectResult? objectResult = result as ObjectResult;

            //assert
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);


        }

        [Fact]
        public async Task VehicleMakeAPIController_DeleteMake_Success()
        {
            //arrange
            int id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleMake>(id)).ReturnsAsync(new VehicleMake() { Name = "Test", Abrv = "Test" });

            //act
            var result = await _controller.DeleteMake(id);

            //assert
            result.Should().BeOfType<OkResult>();
        }


        [Fact]
        public async Task VehicleMakeAPIController_DeleteMake_Fail()
        {

            //########## bad request ###############
            //arrange 
            int id = 0;

            //act
            var result = await _controller.DeleteMake(id);

            //assert
            result.Should().BeOfType<BadRequestResult>();

            //########## vehicle model not found ###############
            //arrange 
            id = 1;
            _vehicleServiceMoq.Setup(x => x.GetById<VehicleMake>(id)).ReturnsAsync(value: null);

            //act
            result = await _controller.DeleteMake(id);
            ObjectResult? objectResult = result as ObjectResult;

            //assert
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
