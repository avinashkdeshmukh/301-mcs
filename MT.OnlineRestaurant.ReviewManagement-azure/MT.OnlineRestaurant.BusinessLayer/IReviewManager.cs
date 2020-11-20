using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public interface IReviewManager
    {
        Task<Review> GetAsync(int userId, int restaurantId);
        Task<int> CreateReviewAsync(Review review);
        Task<bool> UpdateAsync(Review review);
    }
}
