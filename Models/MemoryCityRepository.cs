public class MemoryCityRepository : ICityRepository
{
    private List<City> _cityList = new List<City>()
 {
  new City() {Name="Rybna",Country="Poland",Population=1000},
  new City() {Name="Krakow",Country="Poland",Population=500000},
  new City() {Name="Lizbona",Country="Portugalia",Population=1000000},
  new City() {Name="Idaho",Country="USA",Population=300000}
 };
    public IEnumerable<City> Cities => _cityList;
    public void AddCity(City city)
    {
        _cityList.Add(city);
    }
    public IEnumerable<City> GetAllCities()
    {
        return Cities;
    }
    public int IfCityExistInRepo(string? city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return 1;
        }
        var _existingcity = Cities.FirstOrDefault(c => c.Name == city);
        if (_existingcity == null)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}