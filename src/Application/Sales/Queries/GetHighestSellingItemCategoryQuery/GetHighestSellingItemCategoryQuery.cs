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

namespace CleanArchitecture.Application.Sales.Queries.GetHighestSellingItemCategoryQuery
{
    public class GetHighestSellingItemCategoryQuery : IRequest<string>
    {
        public DateTime TimeRange { get; set; }
    }

    public class GetHighestSellingItemCategoryQueryHandler : IRequestHandler<GetHighestSellingItemCategoryQuery, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHighestSellingItemCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Handle(GetHighestSellingItemCategoryQuery request, CancellationToken cancellationToken)
        {
            var sales = await _context.SalesRecord
                .Where(x => x.SalesDate.Month.Equals(request.TimeRange.Month)
                    && x.SalesDate.Year.Equals(request.TimeRange.Year))
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            int totalSalesOfItem = 0;
            Item highestSellingItem = new Item();
            string highestSellingItemCategory = "";

            foreach (SalesDto s in sales)
            {
                foreach (SalesItemListDto d in s._salesItemList)
                {
                    if (d.Quantity > totalSalesOfItem)
                    {
                        totalSalesOfItem = d.Quantity;
                        highestSellingItem = _context.Items.Where(x => x.ItemId.Equals(d.ItemId)).FirstOrDefault();
                        highestSellingItemCategory = highestSellingItem.ItemCategory.GetDescriptionSplit();
                    }
                }
            }
            if (highestSellingItem == null) return "Null";
            return highestSellingItemCategory;
        }
    }
}
