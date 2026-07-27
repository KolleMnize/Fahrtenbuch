using Fahrtenbuch.Application.Mapper;
using Fahrtenbuch.Application.Tests.Mocks;
namespace Fahrtenbuch.Application.Tests.Mapper;

[TestClass]
public partial class CarDtoMapperTestsCar
{
    [TestMethod]
    public void CarToCarDtoTest()
    {
        // arrange
        var car = MockFactory.CreateMockedCar();

        // act
        var carDto = CarDtoMapper.CarToCarDto(car);

        // assert
        Assert.IsNotNull(carDto);
        Assert.AreEqual(car.Id.Value, carDto.Id);
        Assert.AreEqual(car.Name, carDto.Name);
    }
}