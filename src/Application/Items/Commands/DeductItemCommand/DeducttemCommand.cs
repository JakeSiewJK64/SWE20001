using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Items.Commands.DeductItemCommand
{
    public class DeductItemCommand : IRequest<int>
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class GetItemsQueryHandler : IRequestHandler<DeductItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<int> Handle(DeductItemCommand request, CancellationToken cancellationToken)
        {
            var item = _context.Items.Where(x => x.ItemId.Equals(request.ItemId)).First();
            if(item == null) throw new Exception($"Item with id {request.ItemId} does not exist or has run out!");
            if(item.Quantity - request.Quantity < 0) throw new Exception($"Insufficient quantity for item {item.ItemName}");
            item.Quantity -= request.Quantity;
            _context.Items.Update(item);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
