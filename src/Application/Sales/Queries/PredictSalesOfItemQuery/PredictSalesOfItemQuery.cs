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
            foreach(SalesDto salesdto in salesOrders)
            {
                foreach(SalesItemListDto s in salesdto._salesItemList)
                {
                    if (s.ItemId.Equals(request.ItemId) && salesdto._salesDate.Month.Equals(DateTime.Now.Month)) totalSales += s.Quantity;
                }
            }
            return totalSales / salesOrders.Count;
        }
    }
}
