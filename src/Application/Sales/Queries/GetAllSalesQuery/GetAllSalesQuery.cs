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

namespace CleanArchitecture.Application.Sales.Queries.GetAllSalesQuery
{
    public class GetAllSalesQuery : IRequest<List<SalesDto>>
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SalesDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SalesDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            if (request.startDate == null || request.endDate == null)
            {
                DateTime date = DateTime.Today;
                request.startDate = new DateTime(date.Year, date.Month, 1);
                request.endDate = request.startDate.AddMonths(1).AddDays(-1);
            }
            var sales = await _context.SalesRecord
                .Where(x => x.SalesDate < request.endDate && x.SalesDate > request.startDate)
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            foreach (SalesDto s in sales)
            {
                if (s._items.Length > 0)
                {
                    var items = new List<SalesItemListDto>();
                    foreach (SalesItemListDto d in s._items.ToObject<List<SalesItemListDto>>())
                    {
                        d.Item = _context.Items.Where(x => x.ItemId.Equals(d.ItemId)).First();
                        items.Add(new SalesItemListDto
                        {
                            Item = d.Item,
                            ItemId = d.ItemId,
                            Quantity = d.Quantity
                        });
                    }
                    s._items = items.ToStringJSON();
                }
            }
            return sales;
        }
    }
}
