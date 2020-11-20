using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.ReviewManagement.Model;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MT.OnlineRestaurant.ReviewManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReivewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;
        private readonly IMapper _mapper;

        [Swashbuckle.AspNetCore.Swagger.]
        public ReivewController(ILogger<ReivewController> logger, IReviewManager reviewManager, IMapper mapper)
        {
            
            this._reviewManager = reviewManager;
            this._mapper = mapper;
        }

        [HttpGet()]
        [Route("{userId}/{restaurentId}")]
        public async Task<IActionResult> GetAsync([FromRoute]int userId, [FromRoute]int restaurentId)
        {
            var review = await _reviewManager.GetAsync(userId, restaurentId);
            if (null != review)
            {
                var reviewModel = _mapper.Map<ReviewModel>(review);
                return Ok(reviewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ReviewModel reviewModel)
        {
            var review = _mapper.Map<Review>(reviewModel);
            var id = await _reviewManager.CreateReviewAsync(review);
            return Ok(id);

        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(ReviewModel reviewModel)
        {
            var review = _mapper.Map<Review>(reviewModel);
            var isSuccess = await _reviewManager.UpdateAsync(review);
            return Ok();
        }
    }
}