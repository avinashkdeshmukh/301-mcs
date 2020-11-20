using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MT.OnlineRestaurant.DataLayer.Context;
using System.Linq;

namespace MT.OnlineRestaurant.DataLayer.Repostitory
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDbContext _reviewDbContext;

        public ReviewRepository(ReviewDbContext reviewDbContext)
        {
            this._reviewDbContext = reviewDbContext;
        }

        public async Task<int> CreateReviewAsync(Review review)
        {
            try
            {
                _reviewDbContext.Review.Add(review);
                await _reviewDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return review.Id;
        }

        public async Task<Review> GetAsync(int userId, int restaurantId)
        {
            //_reviewDbContext.Review.FirstOrDefault(reivew=>reivew.UserId == userId && reivew.RestaurentId == restaurantId)
            var result =  (from review in _reviewDbContext.Review
                         where review.RestaurentId == restaurantId && review.UserId == userId
                         select review).ToList().FirstOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateAsync(Review review)
        {
             _reviewDbContext.Review.Update(review);
            await _reviewDbContext.SaveChangesAsync();
            return true;
        }
    }
}
