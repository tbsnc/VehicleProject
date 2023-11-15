using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Service;
using VehicleProject.Data.DTOs;
using VehicleProject.Data.Interfaces;
using VehicleProject.Entity.Models;


namespace VehicleProject.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public  VehicleAPIController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Route("GetAllVehicleMake")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMake>>> GetAllVehicleMake()
        {
            var vehicleMake = await _unitOfWork.vehicleMakeRepo.GetAll();
            var vehicleMakeDto = _mapper.Map<IEnumerable<VehicleMake>>(vehicleMake);

            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            return Ok(vehicleMakeDto);
           
        }

        [HttpGet]
        [Route("GetAllVehicleModel")]
        
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetAllVehicleModel()
        {
            var vehicleModel = await _unitOfWork.vehicleModelRepo.GetAll();
            var vehicleModelDto = _mapper.Map<IEnumerable<VehicleModel>>(vehicleModel);

            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            return Ok(vehicleModelDto);

        }



        [HttpPost]
        [Route("AddVehicleMake")]
        public async Task<ActionResult> AddVehicleMake(VehicleMakeDTO vehicleMakeDTO)
        {
            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDTO);
            
            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            await _unitOfWork.AddAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
            return StatusCode(StatusCodes.Status201Created,"Created");
        }


        [HttpPost]
        [Route("AddVehicleModel")]
        public async Task<ActionResult> AddVehicleModel(VehicleModelDTO vehicleModelDTO)
        {
            var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelDTO);

            if (vehicleModel == null || vehicleModel.MakeId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            await _unitOfWork.AddAsync(vehicleModel);
            await _unitOfWork.CommitAsync();
            return StatusCode(StatusCodes.Status201Created, "Created");
        }


        [HttpDelete("{id}")]
        [Route("DeleteVehicleMake")]
        public async Task<ActionResult> DeleteVehicleMake([FromRoute] long id)
        {
            if (id == 0) return BadRequest();
            var vehicleMake = await _unitOfWork.vehicleMakeRepo.GetById(id);
            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,"Error");
            }

            //delete all dependent models if we are deleting make
            var vehicleModels = await _unitOfWork.vehicleModelRepo.GetAll();
            
            foreach(var vehicleModel in vehicleModels.Where(x=> x.MakeId == id))
            {
                await _unitOfWork.DeleteAsync(vehicleModel);
            }

            await _unitOfWork.DeleteAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        [Route("DeleteVehicleModel")]
        public async Task<ActionResult> DeleteVehicleModel([FromRoute] long id)
        {
            if (id == 0) return BadRequest();
            var vehicleModel = await _unitOfWork.vehicleModelRepo.GetById(id);
            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error");
            }

            await _unitOfWork.DeleteAsync(vehicleModel);
            await _unitOfWork.CommitAsync();
            return Ok();

        }
        [HttpPut("{id}")]
        [Route("UpdateVehicleMake")]
        public async Task<ActionResult> UpdateVehicleMake(int id, VehicleMakeDTO vehicleMakeDto)
        {
            if (vehicleMakeDto == null) return BadRequest();
            
            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);

            if(vehicleMake == null) return StatusCode(StatusCodes.Status500InternalServerError,"Error");    

            vehicleMake.Id = id;

            await _unitOfWork.UpdateAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        [Route("UpdateVehicleModel")]
        public async Task<ActionResult> UpdateVehicleModel(int id, VehicleModelDTO vehicleModelDto)
        {
            if (vehicleModelDto == null) return BadRequest();

            var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelDto);

            if (vehicleModel == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error");

            vehicleModel.Id = id;

            await _unitOfWork.UpdateAsync(vehicleModel);
            await _unitOfWork.CommitAsync();
            return Ok();
        }
    }
}
