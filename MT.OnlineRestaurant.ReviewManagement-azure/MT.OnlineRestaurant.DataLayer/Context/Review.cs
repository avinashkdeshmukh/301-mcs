using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public class Review
    {
        public int Id { get; set; }
        public int RestaurentId { get; set; }
        public int UserId { get; set; }
        public int RatingId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual Rating Rating { get; set; }
    }
}
