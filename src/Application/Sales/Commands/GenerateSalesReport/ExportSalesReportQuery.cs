using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Commands.GenerateSalesReport
{
    public class ExportSalesReportQuery : IRequest<(byte[], string)>
    {
        public DateTime Date { get; set; }
    }

    public class ExportSalesReportQueryHandler : IRequestHandler<ExportSalesReportQuery, (byte[], string)>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportSalesReportQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<(byte[], string)> Handle(ExportSalesReportQuery request, CancellationToken cancellationToken)
        {
            var records = await _context.SalesRecord
                    .Where(d => d.SalesDate.Month == request.Date.Month && d.SalesDate.Year == request.Date.Year)
                    .ProjectTo<SalesDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            var itemListings = await _context.Items.ToListAsync();

            var csvFile = ReportServices.GenerateCSVReport(itemListings, records, request.Date);
            return (csvFile.Item1, csvFile.Item2);
        }
    }
}
