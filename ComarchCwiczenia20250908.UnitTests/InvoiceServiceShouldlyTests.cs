using ComarchCwiczenia20250908.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class InvoiceServiceShouldlyTests
{
    private InvoiceService cut;

    [SetUp]
    public void Setup()
    {
        cut = new InvoiceService();
    }

    [Test]
    public void GenerateInvoiceNumber_ShouldStartWith_INV()
    {
        cut.GenerateInvoiceNumber().ShouldStartWith("INV-");
        
        //var acual = cut.GenerateInvoiceNumber();
        //acual.ShouldStartWith("INV-");
    }

    [Test]
    public void GenerateInvoiceNumber_ShouldEndWithNumeriSuffix()
    {
        cut.GenerateInvoiceNumber().ShouldMatch(@"^INV-\d{8}-\d{3}$");
    }

    [Test]
    public void GenerateInvoiceNumber_ShouldContain_CurrentDate()
    {
        var currentDate = DateTime.Now.ToString("yyyyMMdd");
        cut.GenerateInvoiceNumber().ShouldContain(currentDate);
    }

    [Test]
    public void GenerateInvoiceNumber_ShouldHaveLength_Of_16()
    {
        cut.GenerateInvoiceNumber().Length.ShouldBe(16);
    }

}