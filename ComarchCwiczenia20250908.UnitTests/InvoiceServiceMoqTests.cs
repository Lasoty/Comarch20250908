using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia20250908.Services;
using Moq;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class InvoiceServiceMoqTests
{
    private Mock<ITaxService> _taxServiceMock;

    [SetUp]
    public void Setup()
    {
        _taxServiceMock = new Mock<ITaxService>();
    }
}
