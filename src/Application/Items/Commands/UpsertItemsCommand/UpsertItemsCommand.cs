using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Items.Commands.UpsertItemsCommand
{
    public class UpsertItemsCommand : IRequest<int>
    {
        public ItemsDto itemsObj { get; set; }
    }

    public class GetItemsQueryHandler : IRequestHandler<UpsertItemsCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<int> Handle(UpsertItemsCommand request, CancellationToken cancellationToken)
        {
            var itemsToAdd = new Item
            {
                ItemCategory = request.itemsObj._itemCategory,
                ItemId = request.itemsObj._itemId,
                Quantity = request.itemsObj._quantity,
                BatchId = request.itemsObj._batchId,
                Manufacturer_Id = request.itemsObj._manufacturer_Id,
                Status = request.itemsObj._status,
                CostPrice = request.itemsObj._costPrice,
                SellPrice = request.itemsObj._sellPrice,
                ItemName = request.itemsObj._itemName,
                ImageUrl = request.itemsObj._imageUrl,
                ManufacturerName = request.itemsObj._manufacturerName,
                Remarks = request.itemsObj._remarks,
                RestockDate = request.itemsObj._restockDate,
                ExpDate = request.itemsObj._expDate,
                IsDeleted = request.itemsObj._isDeleted,
                EditedOn = request.itemsObj._editedOn,
                EditedByWho = request.itemsObj._editedBy,
                CreatedByWho = request.itemsObj._createdBy
            };
            var exists = await _context.Items.AnyAsync(x => x.ItemId.Equals(itemsToAdd.ItemId), cancellationToken);
            if (exists) _context.Items.Update(itemsToAdd);
            else _context.Items.Add(itemsToAdd);

            await _context.SaveChangesAsync(cancellationToken);
            return request.itemsObj._itemId;
        }
    }
}
