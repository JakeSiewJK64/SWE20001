using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery;
using CleanArchitecture.Application.Sales.Queries.GetSalesByIdQuery;
using CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    public class SalesListController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetAllSalesRecordsQuery()
        {
            return await Mediator.Send(new GetAllSalesQuery());
        }

        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetSalesByIdQuery(string searchCriteria) 
            => Ok(await Mediator.Send(new GetSalesByIdQuery { SearchCriteria = searchCriteria }));

        [HttpPost]
        public async Task<ActionResult<int>> UpsertSalesCommand(SalesDto salesDto)
            => Ok(await Mediator.Send(new UpsertSalesCommand { salesObj = salesDto }));
    }
}
