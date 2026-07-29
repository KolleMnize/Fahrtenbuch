using ErrorOr;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Tests.sharedkernel;

[TestClass]
public sealed partial class CarIdTests
{
    [TestMethod]
    [TestCategory("Method")]
    public void Create_ShouldReturnErrorWhenGuidIsEmpty()
    {
        // arrange
        var emptyGuid = Guid.Empty;

        // act
        var result = CarId.Create(emptyGuid);

        // assert
        Assert.IsTrue(result.IsError);
        Assert.AreEqual(ErrorType.Validation, result.FirstError.Type);
    }

    [TestMethod]
    [TestCategory("Method")]
    public void Create_ShouldReturnErrorWhenGuidIsNull()
    {
        // arrange
        Guid? nullGuid = null;

        // act
        var result = CarId.Create(nullGuid ?? Guid.Empty);

        // assert
        Assert.IsTrue(result.IsError);
        Assert.AreEqual(ErrorType.Validation, result.FirstError.Type);
    }

    [TestMethod]
    [TestCategory("Method")]
    public void Create_ShouldReturnCarIdWhenGuidIsValid()
    {
        // arrange
        var validGuid = Guid.NewGuid();

        // act
        var result = CarId.Create(validGuid);

        // assert
        Assert.IsFalse(result.IsError);
        Assert.AreEqual(validGuid, result.Value.Value);
    }
}
