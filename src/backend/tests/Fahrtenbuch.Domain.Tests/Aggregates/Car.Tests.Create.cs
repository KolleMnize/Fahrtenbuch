using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Tests.aggregates;

public sealed partial class CarTests
{
    [TestMethod]
    public void Create_ShouldReturnCarWhenCarIdAndNameAreValid()
    {
        // arrange
        var carId = _validCarId;
        var name = _validName;

        // act
        var result = Car.Create(carId, name);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(carId, result.Id);
        Assert.AreEqual(name, result.Name);
    }
}
