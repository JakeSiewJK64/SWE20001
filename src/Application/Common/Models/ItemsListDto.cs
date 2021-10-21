using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models
{
    public class ItemsListDto
    {

        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

    }
}
