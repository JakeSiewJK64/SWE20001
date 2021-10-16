using System;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities
{
    public class Item : AuditableEntity
    {
        private int _itemId, _quantity, _batchId, _manufacturer_Id;
        private Status _status;
        private ItemCategory _itemCategory;
        private float _costPrice, _sellPrice;
        private string _itemName, _manufacturerName, _remarks;
        private DateTime _restockDate, _expDate;
        private bool _isDeleted;

        public Item() { }
        public ItemCategory ItemCategory { get => _itemCategory; set => _itemCategory = value; }
        public int ItemId { get => _itemId; set => _itemId = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public int BatchId { get => _batchId; set => _batchId = value; }
        public int Manufacturer_Id { get => _manufacturer_Id; set => _manufacturer_Id = value; }
        public Status Status { get => _status; set => _status = value; }
        public float CostPrice { get => _costPrice; set => _costPrice = value; }
        public float SellPrice { get => _sellPrice; set => _sellPrice = value; }
        public string ItemName { get => _itemName; set => _itemName = value; }
        public string ManufacturerName { get => _manufacturerName; set => _manufacturerName = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
        public DateTime RestockDate { get => _restockDate; set => _restockDate = value; }
        public DateTime ExpDate { get => _expDate; set => _expDate = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }
    }
}
