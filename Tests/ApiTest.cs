using Moq;
using Xunit;
using Cities.Controllers.Api;
using Microsoft.AspNetCore.Mvc;

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
        City city = new City () { Name = "", Country = "Poland", Population = 800 };
        CityApiController target = new CityApiController(repo);

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
        City city = new City () { Name = "Stalowa Wola", Country = "Poland", Population = 800 };
        CityApiController target = new CityApiController(repo);

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
        City city = new City () { Name = "Rybna", Country = "Poland", Population = 1000 };
        CityApiController target = new CityApiController(repo);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}