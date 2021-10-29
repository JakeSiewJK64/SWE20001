using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CleanArchitecture.Application.Common.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Helpers;

namespace CleanArchitecture.Application.Items.Queries.GetItemsBySearchCriteria
{
    public class GetItemsBySearchCriteriaQuery : IRequest<List<ItemsDto>>
    {
        public string SearchCriteria { get; set; }
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsBySearchCriteriaQuery, List<ItemsDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemsDto>> Handle(GetItemsBySearchCriteriaQuery request, CancellationToken cancellationToken)
        {
            int n;
            var inventory = new List<ItemsDto>();

            if (int.TryParse(request.SearchCriteria, out n))
            {
                inventory = await _context.Items
                    .Where(x => x.ItemId == int.Parse(request.SearchCriteria) && !x.IsDeleted)
                    .ProjectTo<ItemsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            else
            {
                inventory = await _context.Items
                    .Where(x => x.ItemName.Contains(request.SearchCriteria) && !x.IsDeleted)
                    .ProjectTo<ItemsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

            foreach (ItemsDto i in inventory)
            {
                var items = _context.Items.Where(x => (x.ItemName.Contains(request.SearchCriteria) && !x.IsDeleted) | (x.ItemId == int.Parse(request.SearchCriteria) && !x.IsDeleted));
                    
                i._items = items.ToStringJSON();
      
            }
            return inventory;
        }
    }

}
