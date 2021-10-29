using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Sales
{
    using static Testing;

    public class UpsertSalesCommandTests : TestBase
    {
        [Test]
        public async Task ShouldCreateSalesRecord()
        {
            var salesId = await SendAsync(new UpsertSalesCommand
            {
                salesObj = new SalesDto()
            });

            var theSales = await SendAsync(new GetAllSalesQuery
            {

            });
            var theSalesId = theSales[0]._salesRecordId;

            var foundSales = await FindAsync<SalesRecord>(theSalesId);
            foundSales.Should().NotBeNull();
            foundSales.SalesRecordId.Should().Equals(theSalesId);
        }

        [Test]
        public async Task ShouldUpdateSalesRecord()
        {
            var salesId = await SendAsync(new UpsertSalesCommand
            {
                salesObj = new SalesDto()
            });

            var theSales = await SendAsync(new GetAllSalesQuery
            {

            });
            var theSalesId = theSales[0]._salesRecordId;

            SalesDto newSalesObj = new SalesDto();
            newSalesObj._isDeleted = false;
            newSalesObj._remarks = "bad";
            newSalesObj._employeeId = "emp1";

            var secondSalesId = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj
            });

            var foundSales = await FindAsync<SalesRecord>(theSalesId);
            foundSales.Should().NotBeNull();
            foundSales.SalesRecordId.Should().Equals(theSalesId);
            foundSales.IsDeleted.Should().Equals(false);
            foundSales.Remarks.Should().Equals(newSalesObj._remarks);
            foundSales.EmployeeId.Should().Equals(newSalesObj._employeeId);
        }
    }
}
