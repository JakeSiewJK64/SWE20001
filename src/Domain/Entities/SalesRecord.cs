using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class SalesRecord
    {
        private int _salesRecordId;
        private int _itemId;
        private int _quantity;
        private string _employeeId;
        private string _remarks;
        private string _items;
        private bool _isDeleted;
        private string _editedBy;
        private string _createdBy;
        private DateTime _salesDate;
        private DateTime _editedOn;
        private DateTime _createdOn;
        public SalesRecord() { }
        public DateTime SalesDate { get => _salesDate; set => _salesDate = value; }
        public DateTime EditedOn { get => _editedOn; set => _editedOn = value; }
        public DateTime CreatedOn { get => _createdOn; set => _createdOn = value; }
        public int ItemId { get => _itemId; set => _itemId = value; }
        public int SalesRecordId { get => _salesRecordId; set => _salesRecordId = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
        public string EmployeeId { get => _employeeId; set => _employeeId = value; }
        public string Items { get => _items; set => _items = value; }
        public string EditedBy { get => _editedBy; set => _editedBy = value; }
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }

    }
}
