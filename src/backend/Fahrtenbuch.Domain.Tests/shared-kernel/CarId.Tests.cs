using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Domain.Tests.sharedkernel;

public sealed partial class CarIdTests
{
    private readonly static Guid _validGuid = Guid.Parse("c1d67190-3a66-4201-a8f8-cd56f6ab10a4");

    [TestMethod]
    [TestCategory("Constructor")]
    public void CarIdTest()
    {
        // arrangeq
        CarId? carId;

        // act
        carId = CreateMockedInstance();

        //assert
        Assert.IsNotNull(carId);
    }

    private CarId CreateMockedInstance()
    {
        return CarId.Create(_validGuid).Value;
    }
}
