using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserByIdQuery(string userId)
            => Ok(await Mediator.Send(new GetUserByIdQuery { UserId = userId }));
    }
}
