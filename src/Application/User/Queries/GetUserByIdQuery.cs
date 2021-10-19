using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.User.Queries
{

    public class GetUserByIdQuery: IRequest<UserModel>
    {
        public string UserId { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetUserAsync(request.UserId);
        }
    }
}
