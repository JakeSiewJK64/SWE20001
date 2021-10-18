using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Sales.Commands.GenerateSalesReport
{
    public class SalesReportFileRecord : IMapFrom<SalesRecord>
    {
        public int SalesRecordId { get; set; }
        public string Date { get; set; }
        public int EmployeeId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public string Items { get; set; }
        public bool IsDeleted { get; set; }
    }
}
