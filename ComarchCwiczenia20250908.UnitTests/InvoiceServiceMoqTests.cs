using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia20250908.Services;
using Moq;
using Shouldly;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class InvoiceServiceMoqTests
{
    private Mock<ITaxService> _taxServiceMock;
    private Mock<IDiscountService> _discountServiceMock;
    private InvoiceService _invoiceService;

    [SetUp]
    public void Setup()
    {
        _taxServiceMock = new Mock<ITaxService>(MockBehavior.Strict);
        _discountServiceMock = new Mock<IDiscountService>();
        _invoiceService = new InvoiceService(_taxServiceMock.Object, _discountServiceMock.Object);
    }

    [Test]
    public void CalculateTota_WhenCalled_VerifiesTaxServiceGetTaxIsCalled()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";
        _discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>(), It.IsAny<string>())).Returns(5m);

        // Act
        _invoiceService.CalculateTotal(amount, customerType);

        // Assert
        _taxServiceMock.Verify(ts => ts.GetTax(It.IsAny<decimal>(), It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void CalculateTotal_WhenCalled_ReturnsExpectedTotal()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";
        //_discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsIn("Regular", "Extended")))
        //    .Returns(10m);
        _discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>(), It.IsAny<string>())).Returns(5m);

        // Act
        var actual = _invoiceService.CalculateTotal(amount, customerType);

        // Assert
        actual.ShouldBe(95m);
    }

    [Test]
    public void CalculateTotal_WhenCalled_UsesCallbackToManipulateAmount()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";
        var capturedDiscount = 0m;

        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>(), It.IsAny<string>())).Returns(5m);
        _discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(5m)
            .Callback<decimal, string>((a, ct) => capturedDiscount = a * 0.1m);

        // Act
        _invoiceService.CalculateTotal(amount, customerType);

        // Assert
        capturedDiscount.ShouldBe(10m);
    }

    [Test]
    public void CalculateTotal_WhenCalled_ThrowsTaxServiceGetTaxException()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";

        _discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(5m);
        _taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>(), It.IsAny<string>()))
            .Throws(new Exception("Tax calculation failed"));

        // Act & Assert
        Should.Throw<Exception>(() => _invoiceService.CalculateTotal(amount, customerType))
            .Message.ShouldBe("Tax calculation failed");
    }

    [Test]
    public void CalculateTotal_CallsServicesInSequence()
    {
        // Arrange
        var amount = 100m;
        var customerType = "Regular";
        var sequence = new MockSequence();

        _discountServiceMock.InSequence(sequence)
            .Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        
        _taxServiceMock.InSequence(sequence)
            .Setup(ts => ts.GetTax(It.IsAny<decimal>(), It.IsAny<string>())).Returns(5m);

        // Act
        var actual = _invoiceService.CalculateTotal(amount, customerType);

        // Assert
        actual.ShouldBe(95m);
    }
}
