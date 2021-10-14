using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class SalesListDto : IMapFrom<SalesRecord>
    {
        public SalesListDto()
        {
            Sales = new List<SalesDto>();
        }
        public int _salesRecordId { get; set; }
        public IList<SalesDto> Sales { get; set; }
    }
}
