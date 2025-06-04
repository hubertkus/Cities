using Moq;
using Xunit;
using Cities.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class ApiTest
{
    private ServiceProvider _provider;
    public ApiTest()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ICityRepository, MemoryCityRepository>();
        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void GetCity_ShouldReturnBadRequestResult_IfCityIsEmpty()
    {
        //Arrange
        var repo = _provider.GetRequiredService<ICityRepository>();
        var city = new City() { Name = "", Country = "Poland", Population = 800 };
        var target = new CityApiController(repo);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public void GetCity_ShouldReturnConflictResult_IfCityNotExist()
    {
        //Arrange
        var repo = _provider.GetRequiredService<ICityRepository>();
        var city = new City() { Name = "Stalowa Wola", Country = "Poland", Population = 800 };
        var target = new CityApiController(repo);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        Assert.Equal(409, conflictResult.StatusCode);
    }

    [Fact]
    public void GetCity_ShouldReturnOkResult_IfCityExist()
    {
        //Arrange
        var repo = _provider.GetRequiredService<ICityRepository>();
        var city = new City() { Name = "Rybna", Country = "Poland", Population = 1000 };
        var target = new CityApiController(repo);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
    [Fact]
    public void PostCity_ShouldReturnBadRequestResult_IfCityIsEmpty()
    {
        //Arrange
        var repo = _provider.GetRequiredService<ICityRepository>();
        var city = new City() { Name = "", Country = "Poland", Population = 800 };
        var target = new CityApiController(repo);
        var allCitiesCount = repo.GetAllCities().Count();

        //Act
        var result = target.PostCity(city);
        var allCitiesCountAfter = repo.GetAllCities().Count();

        //Asset
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal(allCitiesCount, allCitiesCountAfter);
    }
    [Fact]
    public void PostCity_ShouldReturnConflictResult_IfCityExist()
    {
        //Arrange
        var cityStorage = new List<City>(); // "symulowana baza danych"
        Mock<ICityRepository> mock = new Mock<ICityRepository>();

        mock.Setup(repo => repo.AddCity(It.IsAny<City>()))
            .Callback<City>(c => cityStorage.Add(c));

        mock.Setup(repo => repo.GetAllCities())
            .Returns(() => cityStorage); // zwraca aktualną zawartość

        mock.Setup(repo => repo.IfCityExistInRepo(It.IsAny<string>()))
            .Returns((string name) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return 1;
                }
                var _existingcity = cityStorage.FirstOrDefault(c => c.Name == name);
                if (_existingcity == null)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            });

        var cityOne = new City() { Name = "Stalowa Wola", Country = "Poland", Population = 800 };
        var cityTwo = new City() { Name = "Stalowa Wola", Country = "Polska", Population = 500 };
        var target = new CityApiController(mock.Object);

        //Act
        target.PostCity(cityOne);
        var resultTwo = target.PostCity(cityTwo);
        var allCities = mock.Object.GetAllCities();

        //Asset
        var conflictResult = Assert.IsType<ConflictObjectResult>(resultTwo);
        Assert.Equal(409, conflictResult.StatusCode);
        Assert.Single(allCities);
    }
    [Fact]
    public void PostCity_ShouldReturnOkResult_IfCityNotExist()
    {
        //Arrange
        var repo = _provider.GetRequiredService<ICityRepository>();
        var city = new City() { Name = "Seatle", Country = "Poland", Population = 800 };
        var target = new CityApiController(repo);
        var allCitiesCount = repo.GetAllCities().Count();

        //Act
        var result = target.PostCity(city);
        var allCitiesCountAfter = repo.GetAllCities().Count();

        //Asset
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(allCitiesCount+1, allCitiesCountAfter);
    }
}