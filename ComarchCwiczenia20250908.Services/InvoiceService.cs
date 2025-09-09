using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia20250908.Services;

internal class InvoiceService
{
    internal string GenerateInvoiceNumber()
    {
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        var randomPart = new Random().Next(100, 999).ToString();
        return $"INV-{datePart}-{randomPart}";
    }
}
