using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Commands.GenerateSalesReport
{
    public class ExportSalesReportQuery : IRequest<ExportSalesReportVm>
    {
        public DateTime Date { get; set; }
    }

    public class ExportSalesReportQueryHandler : IRequestHandler<ExportSalesReportQuery, ExportSalesReportVm>
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

        public async Task<ExportSalesReportVm> Handle(ExportSalesReportQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportSalesReportVm();
            var records = await _context.SalesRecord
                    .Where(d => d.SalesDate.Month == request.Date.Month)
                    .ProjectTo<SalesReportFileRecord>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            vm.Content = _fileBuilder.BuildTodoItemsFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "SalesReport.csv";

            return await Task.FromResult(vm);
        }
    }
}
