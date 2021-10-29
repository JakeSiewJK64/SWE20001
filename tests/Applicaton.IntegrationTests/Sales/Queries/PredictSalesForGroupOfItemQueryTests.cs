using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.PredictSalesForGroupOfItemQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class PredictSalesForGroupOfItemQueryTests : TestBase
    {
        [Test]
        public async Task ShouldGetCorrectPredictedNumberForGroupOfItem()
        {
            DateTime todayDate = DateTime.Now;
            SalesDto newSalesObj = new SalesDto();
            List<SalesItemListDto> newSalesItemList = new List<SalesItemListDto>();
            SalesItemListDto newSalesItem = new SalesItemListDto();
            newSalesObj._salesDate = todayDate;
            newSalesItem.ItemId = 1;
            newSalesItem.Quantity = 5;
            newSalesItemList.Add(newSalesItem);
            newSalesObj._salesItemList = newSalesItemList;
            var salesId = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj
            });

            SalesDto newSalesObj2 = new SalesDto();
            List<SalesItemListDto> newSalesItemList2 = new List<SalesItemListDto>();
            SalesItemListDto newSalesItem2 = new SalesItemListDto();
            newSalesObj2._salesDate = todayDate;
            newSalesItem2.ItemId = 1;
            newSalesItem2.Quantity = 3;
            newSalesItemList2.Add(newSalesItem2);
            newSalesObj2._salesItemList = newSalesItemList2;
            var salesId2 = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj2
            });

            List<int> newInts = new List<int>();
            newInts.Add(1);
            var predictedSales = await SendAsync(new PredictSalesForGroupOfItemQuery
            {
                ItemIds = newInts,
                CurrentDate = todayDate
            });

            predictedSales[0].PredictedSales.Should().Equals(4);

        }
    }
}
