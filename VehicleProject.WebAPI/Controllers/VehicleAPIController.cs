using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.Data.DTOs;
using VehicleProject.Data.Interfaces;
using VehicleProject.Entity.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleProject.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VehicleAPIController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VehicleAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET api/<VehicleAPIController>/5
        [HttpGet]
        public async Task<ActionResult> GetAllVehicleMake()
        {
            var vehicleMake = await _unitOfWork.vehicleMakeRepo.GetAll();
            var vehicleMakeDto = _mapper.Map<IEnumerable<VehicleMakeDTO>>(vehicleMake);

            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            return Ok(vehicleMakeDto);
        }
        
        // GET api/<VehicleAPIController>/5
        [HttpGet]
        public async Task<ActionResult> GetAllVehicleModel()
        {
            var vehicleModel = await _unitOfWork.vehicleModelRepo.GetAll();
            var vehicleModelDto = _mapper.Map<IEnumerable<VehicleModelDTO>>(vehicleModel);

            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            return Ok(vehicleModelDto);
        }




        // POST api/<VehicleAPIController>
        [HttpPost]
        public async Task<ActionResult> AddMake(VehicleMakeDTO vehicleMakeDto)
        {
            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);

            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            await _unitOfWork.AddAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
            return StatusCode(StatusCodes.Status201Created, "Created");
        }

        [HttpPost]
        public async Task<ActionResult> AddModel(VehicleModelDTO vehicleModelDTO)
        {
            var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelDTO);

            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            await _unitOfWork.AddAsync(vehicleModel);
            await _unitOfWork.CommitAsync();
            return StatusCode(StatusCodes.Status201Created, "Created");
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMake(int id, VehicleMakeDTO vehicleMakeDto)
        {
            try
            {


                if (id == 0) return BadRequest();

                var vehicleMake = await _unitOfWork.vehicleMakeRepo.GetById(id);

                //test 
                if (vehicleMake == null) return StatusCode(StatusCodes.Status404NotFound, "Error");

                var vehicleMakeUpdate = _mapper.Map<VehicleMake>(vehicleMakeDto);

                vehicleMakeUpdate.Id = id;

                await _unitOfWork.UpdateAsync(vehicleMakeUpdate);

                await _unitOfWork.CommitAsync();

                return Ok();
            } 
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModel(int id, VehicleModelDTO vehicleModelDto)
        {
            try
            {

                if (id == 0) return BadRequest();

                var vehicleModel = await _unitOfWork.vehicleModelRepo.GetById(id);

                //test 
                if (vehicleModel == null) return StatusCode(StatusCodes.Status404NotFound, "Error");

                var vehicleModelUpdate = _mapper.Map<VehicleModel>(vehicleModelDto);

                vehicleModelUpdate.Id = id;

                await _unitOfWork.UpdateAsync(vehicleModelUpdate);

                await _unitOfWork.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        // DELETE api/<VehicleAPIController>/5
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteMake(int id)
        {
            if (id == 0) return BadRequest();
            var vehicleMake = await _unitOfWork.vehicleMakeRepo.GetById(id);
            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error");
            }

            //delete all dependent models if we are deleting make
            var vehicleModels = await _unitOfWork.vehicleModelRepo.GetAll();

            foreach (var vehicleModel in vehicleModels.Where(x => x.MakeId == id))
            {
                await _unitOfWork.DeleteAsync(vehicleModel);
            }

            await _unitOfWork.DeleteAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModel(int id)
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
    }
}
