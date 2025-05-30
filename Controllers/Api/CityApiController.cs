

using Microsoft.AspNetCore.Mvc;

namespace Cities.Controllers.Api
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CityApiController : ControllerBase
    {

        private ICityRepository _repository;
        public CityApiController(ICityRepository repository) => _repository = repository;

        [HttpGet("{city}")]
        public IActionResult GetCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("Nazwa miasta nie może być pusta");
            }
            var _existingcity = _repository.Cities.FirstOrDefault(c => c.Name == city);
            if (_existingcity == null)
            {
                return Conflict($"Miasto: {city} nie istnieje");
            }
            else
            {
                return Ok($"Twoje miasto to: {city}");
            }
        }
        [HttpPost]
        public IActionResult PostCity([FromBody] City city)
        {
            if (string.IsNullOrWhiteSpace(city?.Name))
            {
                return BadRequest("Nazwa miasta nie może być pusta");
            }
            var _existingcity = _repository.Cities.FirstOrDefault(c => c.Name == city.Name);
            if (_existingcity == null)
            {
                _repository.AddCity(city);
                return Ok($"Dodano miasto: {city.Name}");
            }
            else
            {
                return Conflict($"Miasto: {city.Name} już istnieje");
            }
        }
    }
}