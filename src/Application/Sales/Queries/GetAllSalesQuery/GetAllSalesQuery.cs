using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery
{
    public class GetAllSalesQuery : IRequest<List<SalesRecord>>
    {
    }

    public class GetSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SalesRecord>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SalesRecord>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            return await _context.SalesRecord.ToListAsync(cancellationToken);
        }
    }
}
