public interface ICityRepository
{
 public IEnumerable<City> Cities { get; }
 public void AddCity(City city);
 public IEnumerable<City> GetAllCities();
}