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
}