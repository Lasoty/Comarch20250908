using ComarchCwiczenia20250908.Services;
using Shouldly;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class InvoiceServiceCollectionTests
{
    private InvoiceService cut;

    [SetUp]
    public void Setup()
    {
        cut = new InvoiceService();
    }

    [Test]
    public async Task GenerateInvoiceItems_ShouldReturn_NonEmptyCollection()
    {
        List<InvoiceItem> items = await cut.GenerateInvoiceItems();
        items.ShouldNotBeEmpty("Lista produktów nie może być pusta.");

        //(await cut.GenerateInvoiceItems()).ShouldNotBeEmpty();
    }

    [Test]
    public async Task GenerateInvoiceItems_ShouldContain_ItemWithSpecificName()
    {
        List<InvoiceItem> items = await cut.GenerateInvoiceItems();
        items.ShouldContain(item => item.ProductName.Equals("Laptop", StringComparison.InvariantCultureIgnoreCase));
    }

    [Test]
    public async Task GenerateInvoiceItems_ShouldHavePositiveQuantity()
    {
        List<InvoiceItem> items = await cut.GenerateInvoiceItems();
        items.ShouldAllBe(item => item.Quantity > 0);
    }

    [Test]
    public async Task GenerateInvoiceItems_ShouldHave_ExactNumberOfItems()
    {
        List<InvoiceItem> items = await cut.GenerateInvoiceItems();
        items.Count.ShouldBe(3);
        items.Count.ShouldBeGreaterThan(2);
        items.Count.ShouldBeLessThan(4);
    }

    [Test]
    public async Task GenerateInvoiceItems_ShouldHave_ItemsInAscendingOrderByQuantity()
    {
        List<InvoiceItem> items = await cut.GenerateInvoiceItems();
        items.Select(i => i.Quantity).ShouldBeInOrder(SortDirection.Ascending);
    }


}