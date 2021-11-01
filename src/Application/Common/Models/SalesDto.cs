using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class SalesDto
    {
        public int _salesRecordId { get; set; }
        public string _employeeId { get; set; }
        public string _items { get; set; }
        public List<SalesItemListDto> _salesItemList
        {
            get { return _items.ToObject<List<SalesItemListDto>>(); }
            set { _items = value.ToStringJSON(); }
        }
        public DateTime _salesDate { get; set; }
        public DateTime _editedOn { get; set; }
        public DateTime _createdOn { get; set; }
        public string _remarks { get; set; }
        public string _createdBy { get; set; }
        public string _editedBy { get; set; }
        public bool _isDeleted { get; set; }
    }
}
