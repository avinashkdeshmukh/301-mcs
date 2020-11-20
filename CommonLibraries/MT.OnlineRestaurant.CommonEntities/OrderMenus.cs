using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.CommonEntities
{
    public class OrderMenus
    {
        public int? MenuId { get; set; }
        public int? Quantity { get; set; }
        public int? QuantitySold { get; set; }
        public decimal Price { get; set; }
    }
}
