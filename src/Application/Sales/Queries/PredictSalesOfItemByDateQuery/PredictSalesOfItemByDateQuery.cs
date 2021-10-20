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

namespace CleanArchitecture.Application.Sales.Queries.PredictSalesOfItemByDateQuery
{
    public class PredictSalesOfItemByDateQuery : IRequest<int>
    {
        public int ItemId { get; set; }
        public DateTime InputDate { get; set; }
    }

    public class PredictSalesOfItemByDateQueryHandler : IRequestHandler<PredictSalesOfItemByDateQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PredictSalesOfItemByDateQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(PredictSalesOfItemByDateQuery request, CancellationToken cancellationToken)
        {
            int inputDate = Convert.ToInt32(request.InputDate.Month);
            int constant = 2;
            int totalSales = 0;
            List<SalesDto> sales = new List<SalesDto>();
            var salesOrders = await _context.SalesRecord
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            foreach (SalesDto salesdto in salesOrders) if (salesdto._salesDate.Month.Equals(inputDate)) sales.Add(salesdto);

            foreach (SalesDto salesdto in sales)
            {
                foreach (SalesItemListDto s in salesdto._salesItemList)
                {
                    if (s.ItemId.Equals(request.ItemId)) totalSales += s.Quantity;
                }
            }
            var averageSales = totalSales / sales.Count;
            return averageSales + constant;
        }
    }
}
