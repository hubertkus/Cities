

using Microsoft.AspNetCore.Mvc;

namespace Cities.Controllers.Api
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CityApiController : ControllerBase
    {

        private ICityRepository _repository;
        public CityApiController(ICityRepository repository) => _repository = repository;

        [HttpGet("{city?}")]
        public IActionResult GetCity(string? city)
        {
            int result_IfCityExistInRepo = _repository.IfCityExistInRepo(city);

            switch (result_IfCityExistInRepo)
            {
                case 1:
                    return BadRequest("Nazwa miasta nie może być pusta");
                case 2:
                    return Conflict($"Miasto: {city} nie istnieje");
                case 3:
                    return Ok($"Twoje miasto to: {city}");
                default:
                    return BadRequest("Zasób nie znaleziony");
            }
        }
        [HttpPost]
        public IActionResult PostCity([FromBody] City city)
        {
            int result_IfCityExistInRepo = _repository.IfCityExistInRepo(city.Name);

            switch (result_IfCityExistInRepo)
            {
                case 1:
                    return BadRequest("Nazwa miasta nie może być pusta");
                case 2:
                    _repository.AddCity(city);
                    return Ok($"Dodano miasto: {city.Name}");
                case 3:
                    return Conflict($"Miasto: {city.Name} już istnieje");
                default:
                    return BadRequest("Zasób nie znaleziony");
            }
        }
    }
}