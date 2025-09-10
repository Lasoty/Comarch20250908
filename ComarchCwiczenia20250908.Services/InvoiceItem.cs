using System.Security.AccessControl;

namespace ComarchCwiczenia20250908.Services;

public class InvoiceItem
{
    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}

public class Invoice
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime IssueDate { get; set; }

    public string CustomerName { get; set; }
    
    public string CustomerEmail { get; set; }

    public List<InvoiceItem> Items { get; set; }
}
