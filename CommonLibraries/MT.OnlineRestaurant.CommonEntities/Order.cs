using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.CommonEntities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderMenus> OrderMenuDetails { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
