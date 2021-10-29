using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using CleanArchitecture.Application.Sales.Commands.GenerateSalesReport;

namespace CleanArchitecture.Application.IntegrationTests.Sales
{
    using static Testing;
    public class GenerateSalesReportUnitTest : TestBase
    {
        [Test]
        public async Task ShouldGenerateSalesReport()
        {
            var sales = await SendAsync(new ExportSalesReportQuery
            {
                Date = new DateTime()
            });
            sales.Should().NotBeNull();
        }
    }
}
