using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class SalesRecord : AuditableEntity
    {
        private int _salesRecordId;
        private int _itemId;
        private int _quantity;
        private string _employeeId;
        private DateTime _salesDate;
        private string _remarks;
        private string _items;
        private bool _isDeleted;
        public SalesRecord() { }
        public int SalesRecordId { get => _salesRecordId; set => _salesRecordId = value; }
        public DateTime SalesDate { get => _salesDate; set => _salesDate = value; }
        public string EmployeeId { get => _employeeId; set => _employeeId = value; }
        public int ItemId { get => _itemId; set => _itemId = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
        public string Items { get => _items; set => _items = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }

    }
}
