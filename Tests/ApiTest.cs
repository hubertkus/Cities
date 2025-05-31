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
        var city = new City { Name = "Rybna", Country = "Poland", Population = 800 };
        CityApiController target = new CityApiController(mockRepo.Object);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public void GetCity_ShouldReturnConflictResult_IfCityNotExist()
    {
        //Arrange
        var mockRepo = new Mock<ICityRepository>();
        var city = new City { Name = "Rybna", Country = "Poland", Population = 800 };
        CityApiController target = new CityApiController(mockRepo.Object);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var conflictResult = Assert.IsType<ConflictResult>(result);
        Assert.Equal(409, conflictResult.StatusCode);
    }

    [Fact]
    public void GetCity_ShouldReturnOkResult_IfCityExist()
    {
        //Arrange
        var mockRepo = new Mock<ICityRepository>();
        var city = new City { Name = "Rybna", Country = "Poland", Population = 800 };
        CityApiController target = new CityApiController(mockRepo.Object);

        //Act
        var result = target.GetCity(city.Name);

        //Asset
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}