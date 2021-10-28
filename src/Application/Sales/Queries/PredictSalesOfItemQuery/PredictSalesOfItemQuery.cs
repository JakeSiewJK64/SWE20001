using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Queries.PredictSalesOfItemQuery
{
    public class PredictSalesOfItemQuery : IRequest<int>
    {
        public int ItemId { get; set; }
        public DateTime CurrentDate { get; set; }
    }

    public class PredictSalesOfItemQueryHandler : IRequestHandler<PredictSalesOfItemQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PredictSalesOfItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(PredictSalesOfItemQuery request, CancellationToken cancellationToken)
        {
            var salesOrders = await _context.SalesRecord
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            int totalSales = 0;
            int totalSalesOfItemInMonth = 0;
            foreach (SalesDto salesdto in salesOrders)
            {
                if (salesdto._salesDate.Month.Equals(request.CurrentDate.Month) &&
                        salesdto._salesDate.Year.Equals(request.CurrentDate.Year))
                {
                    foreach (SalesItemListDto s in salesdto._salesItemList)
                    {
                        if (s.ItemId.Equals(request.ItemId))
                        {
                            totalSales += s.Quantity;
                            totalSalesOfItemInMonth++;
                        }
                    }
                }
            }
            if (totalSales == 0 || totalSalesOfItemInMonth == 0) return 0;
            return totalSales / totalSalesOfItemInMonth;
        }
    }
}
