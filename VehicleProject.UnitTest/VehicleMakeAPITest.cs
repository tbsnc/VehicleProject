using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using VehicleProject.Repository.Common;
using FluentAssertions;
using FluentAssertions.Execution;
using VehicleProject.WebAPI.Controllers;
using VehicleProject.Service.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.Common.Helper;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;
using VehicleProject.UnitTest.ClassData;
using Microsoft.AspNetCore.Http;
using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;

namespace VehicleProject.UnitTest
{
    public class VehicleMakeAPITest
    {
        private readonly Mock<IVehicleService> _vehicleServiceMoq;
        private readonly IMapper _mapperMoq;
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

            
        }


        [Fact]
        public async Task VehicleMakeAPIController_GetAll_Success()
        {
            //arrange
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);


            //act
            var result = await handler.GetAllVehicleMake();


            //assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory,ClassData(typeof(VehicleMakeClassData))]
      
        public async Task VehicleMakeAPIController_AddMake_Success(VehicleMakeDTO vehicleMakeDto)
        {
            //arrange
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object,_mapperMoq);

            //act
            var result =  await handler.AddMake(vehicleMakeDto);
            ObjectResult? objectResult = result as ObjectResult;
            //assert
            TransactionScope scope = new TransactionScope();

            result.Should().NotBeNull();
            objectResult.Should().NotBeNull();

            objectResult.StatusCode.Should().Be(201);

        }

        [Fact]
        public async Task VehicleMakeAPIController_AddMake_BadRequest()
        {
            
            VehicleMakeDTO vehicleMakeDtoNULL = null;
            VehicleMakeDTO vehicleMakeDtoEMPTY = new VehicleMakeDTO()
            {
                Name = string.Empty,
                Abrv = string.Empty
            };

            //arrange
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);


            //act
            
            var result = await handler.AddMake(vehicleMakeDtoNULL);
            ObjectResult? objectResult = result as ObjectResult;


            //assert
           
            result.Should().NotBeNull();
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(400);

        }


        [Fact]
        public async Task VehicleMakeAPIController_UpdateMake_Success()
        {
            //arrange
            int id = 1;
            VehicleMakeDTO vehicleMakeDTO = new VehicleMakeDTO() { Name = "Test", Abrv = "Test"};
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);
            var vehicleMake = _mapperMoq.Map<VehicleMake>(vehicleMakeDTO);
            
            _vehicleServiceMoq.Setup(s => s.GetById<VehicleMake>(id)).ReturnsAsync(vehicleMake);


            //act
            var result = await handler.UpdateMake(id, vehicleMakeDTO);


            //assert
            result.Should().BeOfType<OkResult>();

        }

        [Fact]
        public async Task VehicleMakeAPIController_UpdateMake_Fail()
        {
            /////////////////vehicleMakeDTO - id error////////////////////////////

            //arrange id = 0
            int id = 0;
            VehicleMakeDTO vehicleMakeDTO = new VehicleMakeDTO() { Name = "Test", Abrv = "Test" };
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);

            //act id = 0
            var result = await handler.UpdateMake(id, vehicleMakeDTO);

            //assert
            result.Should().BeOfType<BadRequestResult>();

            /////////////////vehicleMakeDTO - empty atributes////////////////////////////

            //arrange vehicleMakeDTO empty atributes

            id = 1;
            vehicleMakeDTO.Name = string.Empty;
            vehicleMakeDTO.Abrv = string.Empty;

            //act
            result = await handler.UpdateMake(id, vehicleMakeDTO);

            //assert
            result.Should().BeOfType<BadRequestResult>();

            ////////////////vehicleMakeDTO - vehicle make not found////////////////////////////
            //arrange
            vehicleMakeDTO = new VehicleMakeDTO() { Name = "Test", Abrv = "Test" };
            id = 1;

            //act
            result = await handler.UpdateMake(id, vehicleMakeDTO);
            ObjectResult? objectResult = result as ObjectResult;
           
            //assert
            objectResult.Should().NotBeNull();

            objectResult.StatusCode.Should().Be(404);


        }

        [Fact]
        public async Task VehicleMakeAPIController_DeleteMake_Success()
        {
            //arrange
            int id = 0;
            var vehicleMake = new VehicleMake();
            _vehicleServiceMoq.Setup(s => s.GetById<VehicleMake>(id)).ReturnsAsync(vehicleMake);
            var handler = new VehicleMakeAPIController(_vehicleServiceMoq.Object, _mapperMoq);

            //act
            var result = await handler.DeleteMake(id);

            //assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
