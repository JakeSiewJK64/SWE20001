using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Helpers;
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

namespace CleanArchitecture.Application.Sales.Queries.PredictSalesByItemTypeQuery
{
    public class PredictSalesByItemType : IRequest<int>
    {
        public string itemCat;
    }
    public class PredictSalesByItemTypeHandler : IRequestHandler<PredictSalesByItemType, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PredictSalesByItemTypeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(PredictSalesByItemType request, CancellationToken cancellationToken)
        {
            var salesOrders = await _context.SalesRecord
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            int totalSales = 0, counter = 0;
            foreach (SalesDto salesdto in salesOrders)
            {
                foreach (SalesItemListDto s in salesdto._salesItemList)
                {
                    s.Item = _context.Items.Where(x => x.ItemId.Equals(s.ItemId)).First();
                    if (s.Item.ItemCategory.GetDescriptionSplit() == request.itemCat && salesdto._salesDate.Month.Equals(DateTime.Now.Month) && salesdto._salesDate.Year.Equals(DateTime.Now.Year))
                    {
                        totalSales += s.Quantity;
                        counter++;
                    }
                }
            }
            if (totalSales == 0 || counter == 0) return 0;
            return totalSales/counter;
        }
    }
}
