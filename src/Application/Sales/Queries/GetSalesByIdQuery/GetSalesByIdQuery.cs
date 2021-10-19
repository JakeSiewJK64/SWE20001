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

namespace CleanArchitecture.Application.Sales.Queries.GetSalesByIdQuery
{
    public class GetSalesByIdQuery : IRequest<List<SalesDto>>
    {
        public string SearchCriteria { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<GetSalesByIdQuery, List<SalesDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SalesDto>> Handle(GetSalesByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sales = await _context.SalesRecord
                    .Where(x => x.SalesRecordId == int.Parse(request.SearchCriteria) ||
                        x.Remarks.Contains(request.SearchCriteria) ||
                        x.EmployeeId.Equals(int.Parse(request.SearchCriteria)) &&
                        !x.IsDeleted
                    )
                    .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                foreach (SalesDto s in sales)
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
                return sales;
            } catch(Exception e)
            {
                return new List<SalesDto>();
            }
        }
    }
}