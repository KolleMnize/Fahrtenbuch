using Fahrtenbuch.Domain.entities;

namespace Fahrtenbuch.Domain.Tests.Entities;

public sealed partial class CarTests
{
    [TestMethod]
    public void Rehydrate_ShouldReturnCarWhenCarIdAndNameAreValid()
    {
        // arrange
        var carId = _validCarId;
        var name = _validName;

        // act
        var result = Car.Rehydrate(carId, name);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(carId, result.Id);
        Assert.AreEqual(name, result.Name);
    }
}
