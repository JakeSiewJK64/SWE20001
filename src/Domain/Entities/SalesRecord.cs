﻿using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class SalesRecord : AuditableEntity
    {
        private int _salesRecordId;
        private int _employeeId;
        private int _itemId;
        private int _quantity;
        private DateTime _date;
        private string _remarks;
        private bool _isDeleted;
        public SalesRecord() { }
        public int SalesRecordId { get => _salesRecordId; set => _salesRecordId = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public int EmployeeId { get => _employeeId; set => _employeeId = value; }
        public int ItemId { get => _itemId; set => _itemId = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Remarks { get => _remarks; set => _remarks = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }

    }
}