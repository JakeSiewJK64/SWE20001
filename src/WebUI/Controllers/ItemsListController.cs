
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Items.GetAllItemsQuery;

using System;

using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Items.Commands.UpsertItemsCommand;

namespace CleanArchitecture.WebUI.Controllers
{
    public class ItemsListController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItemsQuery()
            => Ok(await Mediator.Send(new GetAllItemsQuery()));

        [HttpPost]
        public async Task<ActionResult<int>> UpsertItemsCommand(ItemsDto itemsDto)
           => Ok(await Mediator.Send(new UpsertItemsCommand { itemsObj = itemsDto }));
    }
}
