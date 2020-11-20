using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.DataLayer.Repostitory
{
    public interface IReviewRepository
    {
        Task<Review> GetAsync(int userId, int restaurantId);
        Task<int> CreateReviewAsync(Review review);
        Task<bool> UpdateAsync(Review review);
    }
}
