using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Items.Commands.UpsertItemsCommand;
using CleanArchitecture.Application.Items.GetAllItemsQuery;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.PredictSalesByItemTypeQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class PredictSalesByItemTypeTests : TestBase
    {
        [Test]
        public async Task ShouldGetCorrectPredictedNumberForSalesByItemType()
        {
            DateTime todayDate = DateTime.Now;
            ItemsDto item = new ItemsDto();
            item._quantity = 50;
            item._itemCategory = ItemCategory.Narcotics;
            var itemId = await SendAsync(new UpsertItemsCommand
            {
                itemsObj = item
            });

            var allItems = await SendAsync(new GetAllItemsQuery
            {

            });

            Item i = new Item();
            i.ItemId = allItems[0].ItemId;
            i.Quantity = 50;
            i.ItemCategory = ItemCategory.Narcotics;

            SalesDto newSalesObj = new SalesDto();
            List<SalesItemListDto> newSalesItemList = new List<SalesItemListDto>();
            SalesItemListDto newSalesItem = new SalesItemListDto();
            newSalesObj._salesDate = todayDate;
            newSalesItem.ItemId = allItems[0].ItemId;
            newSalesItem.Item = i;
            newSalesItem.Quantity = 8;
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
            newSalesItem2.Item = i;
            newSalesItem2.Quantity = 6;
            newSalesItemList2.Add(newSalesItem2);
            newSalesObj2._salesItemList = newSalesItemList2;
            var salesId2 = await SendAsync(new UpsertSalesCommand
            {
                salesObj = newSalesObj2
            });

            var predictedSales = await SendAsync(new PredictSalesByItemType
            {
                itemCat = ItemCategory.Narcotics.ToString()
            });

            predictedSales.Should().Equals(7);

        }
    }
}
