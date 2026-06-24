using Fahrtenbuch.Domain.entities;
using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Domain.Tests.Entities;

[TestClass]
public sealed partial class CarTests
{
    private readonly static CarId _validCarId = CarId.Create(Guid.Parse("c1d67190-3a66-4201-a8f8-cd56f6ab10a4")).Value;
    private readonly static string _validName = "Test Car";

    [TestMethod]
    [TestCategory("Constructor")]
    public void CarTest()
    {
        // arrange
        Car? car;

        // act
        car = CreateMockedInstance();

        //assert
        Assert.IsNotNull(car);
    }

    private Car CreateMockedInstance()
    {
        return Car.Create(_validCarId, _validName);
    }

}
