using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ReviewManagement.Model
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int RatingId { get; set; }
        public int RestaurentId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
    }
}
