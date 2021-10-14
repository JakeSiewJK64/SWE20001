using System.Collections.Generic;

namespace CleanArchitecture.Application.Sales.Commands.GetSales
{
    public class SalesVm
    {
        public IList<PriorityLevelDto> PriorityLevels { get; set; }

        public IList<SalesListDto> Lists { get; set; }
    }
}
