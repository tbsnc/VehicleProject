using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;
using VehicleProject.Service.Common;


namespace VehicleProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleModelAPIController : ControllerBase
    {

        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        public VehicleModelAPIController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET api/<VehicleAPIController>/<ActionName>/
        [HttpGet]
        public async Task<ActionResult> GetAllVehicleModel()
        {
            var vehicleModel = await _vehicleService.GetAll<VehicleModel>();
            var vehicleModelDto = _mapper.Map<IEnumerable<VehicleModelDTO>>(vehicleModel);

            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            return Ok(vehicleModelDto);
        }

        // POST api/<VehicleAPIController>/<ActionName>/{vehicleMake}
        [HttpPost]
        public async Task<ActionResult> AddModel(VehicleModelDTO vehicleModelDTO)
        {
            var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelDTO);

            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            await _vehicleService.AddAsync(vehicleModel);
            await _vehicleService.CommitAsync();
            return StatusCode(StatusCodes.Status201Created, "Created");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModel(int id, VehicleModelDTO vehicleModelDto)
        {
            try
            {

                if (id == 0) return BadRequest();

                var vehicleModel = await _vehicleService.GetById<VehicleModel>(id);

                //test 
                if (vehicleModel == null) return StatusCode(StatusCodes.Status404NotFound, "Error");

                var vehicleModelUpdate = _mapper.Map<VehicleModel>(vehicleModelDto);

                vehicleModelUpdate.Id = id;

                await _vehicleService.UpdateAsync(vehicleModelUpdate);

                await _vehicleService.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        // DELETE api/<VehicleAPIController>/<ActionName>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModel(int id)
        {

            if (id == 0) return BadRequest();
            var vehicleModel = await _vehicleService.GetById<VehicleModel>(id);
            if (vehicleModel == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error");
            }

            await _vehicleService.DeleteAsync(vehicleModel);
            await _vehicleService.CommitAsync();
            return Ok();

        }

        [HttpGet("filter")]
        public async Task<ActionResult> SearchByQueryString([FromQuery] string s = "",
                                             [FromQuery] string orderby = "asc",
                                             [FromQuery] int per_page = 0,
                                             [FromQuery] int page = 0)
        {
            try
            {
                var movies = await _vehicleService.QueryStringFilter<VehicleModel>(s, orderby, per_page, page);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
    }
}
