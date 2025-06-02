using Moq;
using Xunit;
using Cities.Controllers.Api;
using Microsoft.AspNetCore.Mvc;

namespace Tests;

public class ApiTest
{
    [Fact]
    public void GetCity_ShouldReturnBadRequestResult_IfCityIsEmpty()
    {
        //Arrange
        var mockRepo = new Mock<ICityRepository>();
        var city = new City { Name = "", Country = "Poland", Population = 800 };
        CityController target = new CityController(mockRepo.Object);

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
        var mockRepo = new Mock<ICityRepository>();
        var city = new City { Name = "Stalowa Wola", Country = "Poland", Population = 800 };
        CityController target = new CityController(mockRepo.Object);

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
        var mockRepo = new Mock<ICityRepository>();
        var city = new City { Name = "Rybna", Country = "Poland", Population = 1000 };
        CityController target = new CityController(mockRepo.Object);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}