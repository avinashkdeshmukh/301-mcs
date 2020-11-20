using AutoMapper;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.ReviewManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ReviewManagement.AutoMapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewModel, Review>();
            CreateMap<Review, ReviewModel>();
        }
    }
}
