using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Commands.DeleteSalesCommand;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery;

namespace CleanArchitecture.Application.IntegrationTests.Sales
{
    using static Testing;

    public class DeleteSalesCommandTests : TestBase
    {
        [Test]
        public async Task ShouldDeleteSalesRecord()
        {
            var salesId = await SendAsync(new UpsertSalesCommand
            {
                salesObj = new SalesDto{ _salesDate = System.DateTime.Now }
            });

            var theSales = await SendAsync(new GetAllSalesQuery
            {
                startDate = System.DateTime.Now.Date.AddDays(-1),
                endDate = System.DateTime.Now.Date.AddDays(1)
            });
            var theSalesId = theSales[0]._salesRecordId;

            await SendAsync(new DeleteSalesCommand
            {
                Id = theSalesId
            });

            var salesResult = await FindAsync<SalesRecord>(theSalesId);

            salesResult.Should().BeNull();
        }
    }
}
