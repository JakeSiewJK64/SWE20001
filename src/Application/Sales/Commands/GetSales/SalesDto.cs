using AutoMapper;
using System;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class SalesDto : IMapFrom<SalesRecord>
    {
        public int _salesRecordId { get; set; }
        public int _employeeId { get; set; }
        private int _itemId { get; set; }
        private int _quantity { get; set; }
        private DateTime _date { get; set; }
        private string _remarks { get; set; }
        private bool _isDeleted { get; set; }

    }
}
