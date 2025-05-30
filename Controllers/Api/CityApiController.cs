

using Microsoft.AspNetCore.Mvc;

namespace Cities.Controllers.Api
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CityController : ControllerBase
    {

        private ICityRepository _repository;
        public CityController(ICityRepository repository) => _repository = repository;

        [HttpGet("{city}")]
        public IActionResult GetCity(string city)
        {
            var _city = _repository.Cities.FirstOrDefault(c => c.Name == city)?.Name;
            var message = $"Twoje miasto to: {_city ?? "brak miasta"}";
            return Ok(message);
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