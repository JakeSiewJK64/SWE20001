using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Models
{
    public class ItemsDto 
    {
        public string _items { get; set; }
        public List<ItemsListDto> _itemsList
        {
            get { return _items.ToObject<List<ItemsListDto>>(); }
            set { }
        }

        public ItemCategory _itemCategory { get; set; }
        public int _itemId { get; set; }
        public int _quantity { get; set; }
        public int _batchId { get; set; }
        public int _manufacturer_Id { get; set; }
        public Status _status { get; set; }
        public float _costPrice { get; set; }
        public float _sellPrice { get; set; }
        public string _itemName { get; set; }
        public string _imageUrl { get; set; }
        public string _manufacturerName { get; set; }
        public string _remarks { get; set; }
        public DateTime _restockDate { get; set; }
        public DateTime _expDate { get; set; }
        public bool _isDeleted { get; set; }

    }
}
