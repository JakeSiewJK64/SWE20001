using AutoMapper;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Queries.UpsertSalesCommand
{
    public class UpsertSalesCommand : IRequest<int>
    {
        public SalesDto salesObj { get; set; }
    }

    public class GetSalesQueryHandler : IRequestHandler<UpsertSalesCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<int> Handle(UpsertSalesCommand request, CancellationToken cancellationToken)
        {
            var salesToAdd = new SalesRecord
            {
                SalesRecordId = request.salesObj._salesRecordId,
                Date = request.salesObj._date,
                CreatedBy = request.salesObj.CreatedBy,
                EmployeeId = request.salesObj._employeeId,
                Remarks = request.salesObj._remarks,
                Items = request.salesObj._salesItemList.ToStringJSON()
            };
            var exists = await _context.SalesRecord.AnyAsync(x => x.SalesRecordId.Equals(salesToAdd.SalesRecordId), cancellationToken);
            if (exists) _context.SalesRecord.Update(salesToAdd);
            else _context.SalesRecord.Add(salesToAdd);

            await _context.SaveChangesAsync(cancellationToken);
            return request.salesObj._salesRecordId;
        }
    }
}
