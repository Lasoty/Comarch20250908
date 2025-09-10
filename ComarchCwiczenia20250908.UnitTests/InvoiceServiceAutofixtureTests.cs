using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using ComarchCwiczenia20250908.Services;
using Shouldly;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class InvoiceServiceAutofixtureTests
{

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
}
