using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;
using VehicleProject.Service.Common;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleProject.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VehicleMakeAPIController : ControllerBase
    {

        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        public VehicleMakeAPIController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }


        // GET api/<VehicleAPIController>/<ActionName>/
        [HttpGet]
        public async Task<ActionResult> GetAllVehicleMake()
        {
            var vehicleMake = await _vehicleService.GetAll<VehicleMake>();
            var vehicleMakeDto = _mapper.Map<IEnumerable<VehicleMakeDTO>>(vehicleMake);

            if (vehicleMake == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
            return Ok(vehicleMakeDto);
        }

       



        // POST api/<VehicleAPIController>/<ActionName>/{vehicleMake}
        [HttpPost]
        public async Task<IActionResult> AddMake(VehicleMakeDTO vehicleMakeDto)
        {
            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);

            if (vehicleMake == null ||vehicleMake.Name.Trim() == string.Empty ||vehicleMake.Abrv.Trim() == string.Empty)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Error");
            }
            await _vehicleService.AddAsync(vehicleMake);
            await _vehicleService.CommitAsync();
            return StatusCode(StatusCodes.Status201Created, "Created");
        }

       



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMake(int id, VehicleMakeDTO vehicleMakeDto)
        {
            try
            {


                if (id == 0 ||
                    vehicleMakeDto == null ||
                    vehicleMakeDto.Name.Trim() == string.Empty ||
                    vehicleMakeDto.Abrv.Trim() == string.Empty
                    ) return BadRequest();

                var vehicleMake = await _vehicleService.GetById<VehicleMake>(id);

                //test 
                if (vehicleMake == null) return StatusCode(StatusCodes.Status404NotFound, "Error");

                var vehicleMakeUpdate = _mapper.Map<VehicleMake>(vehicleMakeDto);

                vehicleMakeUpdate.Id = id;

                await _vehicleService.UpdateAsync(vehicleMakeUpdate);

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

        public async Task<ActionResult> DeleteMake(int id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var vehicleMake = await _vehicleService.GetById<VehicleMake>(id);
                if (vehicleMake == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Error");
                }

                //delete all dependent models if we are deleting make
                var vehicleModels = await _vehicleService.GetAll<VehicleModel>();

                foreach (var vehicleModel in vehicleModels.Where(x => x.MakeId == id))
                {
                    await _vehicleService.DeleteAsync(vehicleModel);
                }

                await _vehicleService.DeleteAsync(vehicleMake);
                await _vehicleService.CommitAsync();
                return Ok();
            }catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult> SearchByQueryString([FromQuery] string s = "",
                                              [FromQuery] string orderby = "asc",
                                              [FromQuery] int per_page = 0,
                                              [FromQuery] int page = 0)
        {
            try
            {
                var movies = await _vehicleService.QueryStringFilter<VehicleMake>(s, orderby, per_page, page);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
    }
}
