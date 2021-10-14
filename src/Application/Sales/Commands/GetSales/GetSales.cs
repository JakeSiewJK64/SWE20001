using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class GetSales : IRequest<SalesVm>
    {
    }

    public class GetSalesQueryHandler : IRequestHandler<GetSales, SalesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalesVm> Handle(GetSales request, CancellationToken cancellationToken)
        {
            return new SalesVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Lists = await _context.TodoLists
                    .ProjectTo<SalesListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(s => s._salesRecordId)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
