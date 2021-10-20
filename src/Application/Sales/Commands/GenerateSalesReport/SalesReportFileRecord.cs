using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using System;

namespace CleanArchitecture.Application.Sales.Commands.GenerateSalesReport
{
    public class SalesReportFileRecord : IMapFrom<SalesRecord>
    {
        public int SalesRecordId { get; set; }
        public DateTime SalesDate { get; set; }
        public string EmployeeId { get; set; }
        public string Remarks { get; set; }
        public string Items { get; set; }
    }
}
