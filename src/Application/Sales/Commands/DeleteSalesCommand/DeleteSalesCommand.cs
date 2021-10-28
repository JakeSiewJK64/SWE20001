using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Sales.Commands.DeleteSalesCommand
{
    public class DeleteSalesCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSalesCommandHandler : IRequestHandler<DeleteSalesCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSalesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSalesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SalesRecord.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(SalesRecord), request.Id);
            }

            _context.SalesRecord.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
