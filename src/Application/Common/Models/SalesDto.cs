using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class SalesDto : AuditableEntity
    {
        public int _salesRecordId { get; set; }
        public int _employeeId { get; set; }
        public int _itemId { get; set; }
        public int _quantity { get; set; }
        public Item[] items { get; set; }
        public string  _date { get; set; }
        public string _remarks { get; set; }
        public bool _isDeleted { get; set; }
    }
}
