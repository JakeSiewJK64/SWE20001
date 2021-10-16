using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class SalesDto : AuditableEntity
    {
        public int _salesRecordId { get; set; }
        public int _employeeId { get; set; }
        public string _items { get; set; }
        public List<SalesItemListDto> _salesItemList
        {
            get { return _items.ToObject<List<SalesItemListDto>>(); }
            set { }
        }
        public string _date { get; set; }
        public string _remarks { get; set; }
        public bool _isDeleted { get; set; }
    }
}
