using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Commands.GenerateSalesReport;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery;
using CleanArchitecture.Application.Sales.Queries.GetSalesByIdQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using CleanArchitecture.Application.Sales.Queries.PredictSalesOfItemQuery;
using System;
using CleanArchitecture.Application.Sales.Queries.PredictSalesForGroupOfItemQuery;
using CleanArchitecture.Application.Sales.Queries.PredictSalesByItemTypeQuery;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesCurrentMonthQuery;
using CleanArchitecture.Application.Sales.Queries.GetHighestItemSalesQuery;
using CleanArchitecture.Application.Sales.Queries.GetHighestSellingItemCategoryQuery;

namespace CleanArchitecture.WebUI.Controllers
{
    public class SalesListController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetAllSalesRecordsQuery(DateTime startDate, DateTime endDate)
            => Ok(await Mediator.Send(new GetAllSalesQuery { startDate = startDate, endDate = endDate}));

        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetSalesBySearchCriteriaQuery(string searchCriteria)
            => Ok(await Mediator.Send(new GetSalesByIdQuery { SearchCriteria = searchCriteria }));

        [HttpPost]
        public async Task<ActionResult<int>> UpsertSalesCommand(SalesDto salesDto)
            => Ok(await Mediator.Send(new UpsertSalesCommand { salesObj = salesDto }));

        [HttpGet]
        public async Task<ActionResult<int>> PredictSalesByItemTypeQuery(string itemCategory)
            => Ok(await Mediator.Send(new PredictSalesByItemType { itemCat = itemCategory }));

        [HttpPost]
        public async Task<ActionResult<List<SalesGroupItemModel>>> PredictSalesForGroupOfItemQuery(List<int> itemId, DateTime _currentDate)
            => Ok(await Mediator.Send(new PredictSalesForGroupOfItemQuery { ItemIds = itemId, CurrentDate = _currentDate }));

        [HttpGet]
        public async Task<ActionResult<int>> PredictSalesOfItemForNextMonthQuery(int itemId, DateTime currentDate)
            => Ok(await Mediator.Send(new PredictSalesOfItemQuery { ItemId = itemId, CurrentDate = currentDate }));

        [HttpGet]
        public async Task<ActionResult<int>> GetTotalSalesForCurrentMonth(DateTime timeRange)
            => Ok(await Mediator.Send(new GetAllSalesCurrentMonthQuery { TimeRange = timeRange }));

        [HttpGet]
        public async Task<ActionResult<string>> GetHighestSellingItemQuery(DateTime timeRange)
            => Ok(await Mediator.Send(new GetHighestItemSalesQuery { TimeRange = timeRange }));
        
        [HttpGet]
        public async Task<ActionResult<string>> GetHighestSellingItemCategoryQuery(DateTime timeRange)
            => Ok(await Mediator.Send(new GetHighestSellingItemCategoryQuery { TimeRange = timeRange }));

        [HttpPost]
        public async Task<FileResult> GenerateMonthlySalesReportCommand([FromBody] ExportSalesReportQuery command)
        {
            var (files, fileName) = await Mediator.Send(command);
            return File(files, "application/octet-stream", fileName);
        }
    }
}
