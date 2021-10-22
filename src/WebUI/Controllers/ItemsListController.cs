
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Items.GetAllItemsQuery;

using System;

using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.WebUI.Controllers
{
    public class ItemsListController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItemsQuery()
            => Ok(await Mediator.Send(new GetAllItemsQuery()));
    }
}
