using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesCurrentMonthQuery;
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

    public class GetAllSalesCurrentMonthQueryTests : TestBase
    {
        [Test]
        public async Task ShouldCreateSalesRecord()
        {
            DateTime todayDate = DateTime.Now;
            DateTime lastMonthDate = todayDate.AddMonths(-1);
            SalesDto newSalesObj = new SalesDto();
            newSalesObj._isDeleted = false;
            newSalesObj._remarks = "bad";
            newSalesObj._employeeId = "emp1";
            newSalesObj._salesDate = todayDate;
            var salesId = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj
            });

            SalesDto newSalesObj2 = new SalesDto();
            newSalesObj2._isDeleted = false;
            newSalesObj2._remarks = "bad";
            newSalesObj2._employeeId = "emp1";
            newSalesObj2._salesDate = lastMonthDate;
            var salesId2 = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj2
            });

            var theSales = await SendAsync(new GetAllSalesCurrentMonthQuery
            {
                TimeRange = todayDate
            });

            theSales.Should().Equals(1);
        }
    }
}
