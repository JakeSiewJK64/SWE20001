using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Models
{
    public class ItemsDto : AuditableEntity
    {
        public string _items { get; set; }
        public List<ItemsListDto> _itemsList
        {
            get { return _items.ToObject<List<ItemsListDto>>(); }
            set { }
        }

    }
}
