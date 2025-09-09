using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia20250908.Services;

internal class InvoiceService
{
    private readonly ITaxService _taxService;
    private readonly IDiscountService _discountService;

    public InvoiceService()
    {
    }

    public InvoiceService(ITaxService taxService, IDiscountService discountService)
    {
        _taxService = taxService;
        _discountService = discountService;
    }

    public decimal CalculateTotal(decimal amount, string customerType)
    {
        var discount = _discountService.CalculateDiscount(amount, customerType);
        var taxable = amount - discount;
        var tax = _taxService.GetTax(taxable, customerType);
        return taxable + tax;
    }

    internal string GenerateInvoiceNumber()
    {
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        var randomPart = new Random().Next(100, 999).ToString();
        return $"INV-{datePart}-{randomPart}";
    }

    public Task<List<InvoiceItem>> GenerateInvoiceItems()
    {
        return Task.FromResult(
            new List<InvoiceItem>
            {
                new() { ProductName = "Laptop", Quantity = 1, UnitPrice = 1000m },
                new() { ProductName = "Smartphone", Quantity = 2, UnitPrice = 500m },
                new() { ProductName = "Tablet", Quantity = 3, UnitPrice = 300m },
            });
    }
}