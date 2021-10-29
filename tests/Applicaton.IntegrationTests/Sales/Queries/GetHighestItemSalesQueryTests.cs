using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Items.Commands.UpsertItemsCommand;
using CleanArchitecture.Application.Items.GetAllItemsQuery;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.GetHighestItemSalesQuery;
using CleanArchitecture.Application.Sales.Queries.PredictSalesOfItemQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using CleanArchitecture.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class GetHighestItemSalesQueryTests : TestBase
    {
        [Test]
        public async Task ShouldGetHighestItemSales()
        {
            DateTime todayDate = DateTime.Now;
            ItemsDto item = new ItemsDto();
            item._quantity = 50;
            item._itemName = "Acetaminophen (Tylenol)";
            item._itemCategory = ItemCategory.Narcotics;
            var itemId = await SendAsync(new UpsertItemsCommand
            {
                itemsObj = item
            });
            ItemsDto item2 = new ItemsDto();
            item2._quantity = 50;
            item2._itemName = "Aspirin";
            item2._itemCategory = ItemCategory.Painkillers;
            var itemId2 = await SendAsync(new UpsertItemsCommand
            {
                itemsObj = item2
            });
            var allItems = await SendAsync(new GetAllItemsQuery
            {

            });

            SalesDto newSalesObj = new SalesDto();
            List<SalesItemListDto> newSalesItemList = new List<SalesItemListDto>();
            SalesItemListDto newSalesItem = new SalesItemListDto();
            newSalesObj._salesDate = todayDate;
            newSalesItem.ItemId = allItems[0].ItemId;
            newSalesItem.Quantity = 3;
            newSalesItemList.Add(newSalesItem);
            newSalesItem.ItemId = allItems[1].ItemId;
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
            newSalesItem2.ItemId = allItems[0].ItemId;
            newSalesItem2.Quantity = 6;
            newSalesItemList2.Add(newSalesItem2);
            newSalesObj2._salesItemList = newSalesItemList2;
            var salesId2 = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj2
            });

            var HighestItemSales = await SendAsync(new GetHighestItemSalesQuery
            {
                TimeRange = todayDate
            });

            HighestItemSales.Should().Be(allItems[0].ItemName);

        }
    }
}
