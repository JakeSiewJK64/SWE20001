using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
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

namespace CleanArchitecture.Application.Sales.Queries.GetSalesByIdQuery
{
    public class GetSalesByIdQuery : IRequest<List<SalesRecord>>
    {
        public string SearchCriteria { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<GetSalesByIdQuery, List<SalesRecord>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SalesRecord>> Handle(GetSalesByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SalesRecord
                    .Where(x => x.SalesRecordId.Equals(int.Parse(request.SearchCriteria))
                        || x.Remarks.Contains(request.SearchCriteria)
                        || x.LastModifiedBy.Contains(request.SearchCriteria)
                        || x.Items.Contains(request.SearchCriteria)
                    ).ToListAsync(cancellationToken);
            } catch(Exception e)
            {
                return new List<SalesRecord>();
            }
        }
    }
}
