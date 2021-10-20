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
using CleanArchitecture.Application.Sales.Queries.PredictSalesOfItemByDateQuery;

namespace CleanArchitecture.WebUI.Controllers
{
    public class SalesListController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetAllSalesRecordsQuery() 
            => Ok(await Mediator.Send(new GetAllSalesQuery()));

        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetSalesByIdQuery(string searchCriteria)
            => Ok(await Mediator.Send(new GetSalesByIdQuery { SearchCriteria = searchCriteria }));

        [HttpPost]
        public async Task<ActionResult<int>> UpsertSalesCommand(SalesDto salesDto)
            => Ok(await Mediator.Send(new UpsertSalesCommand { salesObj = salesDto }));
        
        [HttpGet]
        public async Task<ActionResult<int>> PredictSalesOfItemForNextMonthQuery(int itemId)
            => Ok(await Mediator.Send(new PredictSalesOfItemQuery { ItemId = itemId }));
        
        [HttpGet]
        public async Task<ActionResult<int>> PredictSalesOfItemByDateQuery(DateTime inputDate, int itemId)
            => Ok(await Mediator.Send(new PredictSalesOfItemByDateQuery { InputDate = inputDate, ItemId = itemId }));

        [HttpGet("{date}")]
        public async Task<FileResult> GenerateMonthlySalesReportCommand(DateTime date)
        {
            var vm = await Mediator.Send(new ExportSalesReportQuery { Date = date });
            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }
}
