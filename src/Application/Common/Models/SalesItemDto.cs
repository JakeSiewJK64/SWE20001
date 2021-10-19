using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models
{
    public class SalesItemListDto
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
