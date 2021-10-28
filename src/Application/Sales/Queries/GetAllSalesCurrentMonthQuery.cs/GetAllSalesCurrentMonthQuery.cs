using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Queries.GetAllSalesCurrentMonthQuery
{
    public class GetAllSalesCurrentMonthQuery : IRequest<int>
    {
        public DateTime TimeRange { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<GetAllSalesCurrentMonthQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetAllSalesCurrentMonthQuery request, CancellationToken cancellationToken)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var sales = await _context.SalesRecord
                .Where(x => x.SalesDate.Month == request.TimeRange.Month && x.SalesDate.Year == request.TimeRange.Year)
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
            return sales.Count();
        }
    }
}
