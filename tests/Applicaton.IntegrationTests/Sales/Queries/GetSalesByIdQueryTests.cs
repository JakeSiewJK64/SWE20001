using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery;
using CleanArchitecture.Application.Sales.Queries.GetSalesByIdQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class GetSalesByIdQueryTests : TestBase
    {
        [Test]
        public async Task ShouldGetCorrectSales()
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
            newSalesObj2._remarks = "good";
            newSalesObj2._employeeId = "emp2";
            newSalesObj2._salesDate = lastMonthDate;
            var salesId2 = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj2
            });

            var allSales = await SendAsync(new GetAllSalesQuery
            {

            });
            var firstSalesId = allSales[0]._salesRecordId;

            var theSales = await SendAsync(new GetSalesByIdQuery
            {
                SearchCriteria = firstSalesId.ToString()
            });

            theSales[0]._isDeleted.Should().Equals(newSalesObj._isDeleted);
            theSales[0]._remarks.Should().Equals(newSalesObj._remarks);
            theSales[0]._employeeId.Should().Equals(newSalesObj._employeeId);
            theSales[0]._salesDate.Should().Equals(newSalesObj._salesDate);

        }
    }
}
