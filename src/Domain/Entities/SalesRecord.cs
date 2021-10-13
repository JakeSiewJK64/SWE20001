using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class SalesRecord : AuditableEntity
    {
        public int _salesId, _employeeId, _itemId, _quantity;
        public DateTime _date;
        public string _remarks;
        public bool _isDeleted;
        public SalesRecord() { }
        public int SalesId { get => _salesId; }
        public DateTime Date { get => _date; }
        public int EmployeeId { get => _employeeId; }
        public int ItemId { get => _itemId; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }
    }
}
