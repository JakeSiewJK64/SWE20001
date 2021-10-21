using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Items.GetAllItemsQuery
{
    public class GetAllItemsQuery : IRequest<List<ItemsDto>>
    {
    }

    public class GetItemsQueryHandler : IRequestHandler<GetAllItemsQuery, List<ItemsDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemsDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items.ProjectTo<ItemsDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
            foreach (ItemsDto i in items)
            {
                var item = new List<ItemsListDto>();
                foreach (ItemsListDto d in i._items.ToObject<List<ItemsListDto>>())
                {
                    d.Item = _context.Items.Where(x => x.ItemId.Equals(d.ItemId)).First();
                    item.Add(new ItemsListDto
                    {
                        Item = d.Item,
                        ItemId = d.ItemId,
                        Quantity = d.Quantity
                    });
                }
                i._items = item.ToStringJSON();
            }
            return items;
        }
    }

}
