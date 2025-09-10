using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ComarchCwiczenia20250908.Services;
using Moq;
using Shouldly;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class InvoiceServiceAutofixtureTests
{
    private IFixture _fixture;
    private Mock<IInvoiceRepository> _invoiceRepositoryMock;
    private Mock<IEmailSender> _emailSender;
    private InvoiceService2 _invoiceService;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _invoiceRepositoryMock = _fixture.Freeze<Mock<IInvoiceRepository>>();
        _emailSender = _fixture.Freeze<Mock<IEmailSender>>();
        _invoiceService = _fixture.Create<InvoiceService2>();
    }

    [Test]
    public void CreateInvoice_ShouldReturn_InvoiceWithItems()
    {
        // Arrange
        var fixture = new Fixture();
        var invoiceService = new InvoiceService();
        var customerName = fixture.Create<string>();
        var items = fixture.CreateMany<InvoiceItem>(3).ToList();

        // Act
        var actual = invoiceService.CreateInvoice(customerName, items);

        // Assert
        actual.ShouldNotBeNull();
        actual.Items.ShouldAllBe(i => !string.IsNullOrEmpty(i.ProductName));
        actual.Amount.ShouldBe(items.Sum(i => i.UnitPrice * i.Quantity));
    }

    [Test]
    public void CreateInvoice_ShouldCreateInvoiceItemWithSpecificPrice()
    {
        // Arrange
        var fixture = new Fixture();
        var invoiceService = new InvoiceService();
        string customerName = fixture.Create<string>();

        fixture.Customize<InvoiceItem>(item => item.With(x => x.UnitPrice, 100m));
        var items = fixture.CreateMany<InvoiceItem>(3).ToList();

        // Act
        var actual = invoiceService.CreateInvoice(customerName, items);

        // Assert
        actual.Items.ShouldAllBe(item => item.UnitPrice == 100m);
    }

    [Test]
    public void SaveInvoice_ShouldSaveInvoiceAndSendEmail()
    {
        // Arrange
        var invoice = _fixture.Create<Invoice>();

        // Act
        _invoiceService.SaveInvoice(invoice);

        // Assert
        _invoiceRepositoryMock.Verify(repo => repo.Save(invoice), Times.Once);
        _emailSender.Verify(sender => sender.Send(invoice.CustomerEmail, 
            "Invoice Created", "Your invoice has beed successfully created."));
    }
}
