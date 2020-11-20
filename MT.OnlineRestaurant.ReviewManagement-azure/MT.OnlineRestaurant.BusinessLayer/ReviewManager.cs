using System;
using System.Threading.Tasks;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.Repostitory;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepository _reivewRepository;

        public ReviewManager(IReviewRepository reivewRepository)
        {
            this._reivewRepository = reivewRepository;
        }
        public async Task<int> CreateReviewAsync(Review review)
        {
            return await _reivewRepository.CreateReviewAsync(review);
        }

        public async Task<Review> GetAsync(int userId, int restaurantId)
        {
            return await _reivewRepository.GetAsync(userId, restaurantId);
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            return await _reivewRepository.UpdateAsync(review);
        }
    }
}
