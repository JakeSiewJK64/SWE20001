using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models
{
    public class SalesGroupItemModel
    {
        public int ItemId { get; set; }
        public int PredictedSales { get; set; }
    }
}
