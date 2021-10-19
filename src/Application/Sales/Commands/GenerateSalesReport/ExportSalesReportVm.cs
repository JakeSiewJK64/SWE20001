namespace CleanArchitecture.Application.Sales.Commands.GenerateSalesReport
{
    public class ExportSalesReportVm
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}