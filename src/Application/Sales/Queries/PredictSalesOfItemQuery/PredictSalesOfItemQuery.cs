using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Queries.PredictSalesOfItemQuery
{
    public class PredictSalesOfItemQuery : IRequest<int>
    {
        public int ItemId { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<PredictSalesOfItemQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(PredictSalesOfItemQuery request, CancellationToken cancellationToken)
        {
            // todo: get the data working for the report
            int returnData = 1;
            return returnData;
        }
    }
}
