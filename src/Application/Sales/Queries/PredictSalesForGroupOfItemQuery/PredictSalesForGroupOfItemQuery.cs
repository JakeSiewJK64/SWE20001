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

namespace CleanArchitecture.Application.Sales.Queries.PredictSalesForGroupOfItemQuery
{
    public class PredictSalesForGroupOfItemQuery : IRequest<List<SalesGroupItemModel>>
    {
        public List<int> ItemIds = new List<int>();
        public DateTime CurrentDate;
    }

    public class PredictSalesForGroupOfItemQueryHandler : IRequestHandler<PredictSalesForGroupOfItemQuery, List<SalesGroupItemModel>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PredictSalesForGroupOfItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SalesGroupItemModel>> Handle(PredictSalesForGroupOfItemQuery request, CancellationToken cancellationToken)
        {
            var salesOrders = await _context.SalesRecord
                .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            List<SalesGroupItemModel> reportItems = new List<SalesGroupItemModel>();
            List<SalesItemListDto> salesItemList = new List<SalesItemListDto>();
            foreach (SalesDto salesdto in salesOrders)
            {
                if (salesdto._salesDate.Month.Equals(request.CurrentDate.Month) && salesdto._salesDate.Year.Equals(request.CurrentDate.Year))
                {
                    foreach (SalesItemListDto s in salesdto._salesItemList)
                    {
                        if (request.ItemIds.Contains(s.ItemId)) salesItemList.Add(s);
                    }
                }
            }
            SalesGroupItemModel itemModel;
            int counter = 0;
            foreach (int i in request.ItemIds)
            {
                itemModel = new SalesGroupItemModel();
                foreach (SalesItemListDto sil in salesItemList)
                {
                    if (sil.ItemId == i)
                    {
                        itemModel.ItemId = sil.ItemId;
                        itemModel.PredictedSales += sil.Quantity;
                        counter++;
                    }
                }
                itemModel.PredictedSales = itemModel.PredictedSales / counter;
                counter = 0;
                reportItems.Add(itemModel);
            }
            return reportItems;
        }
    }
}
