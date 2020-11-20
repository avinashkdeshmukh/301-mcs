using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer.Repostitory;
using MT.OnlineRestaurant.ReviewManagement.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ReviewManagement.Extension
{
    public static class CollectionServiceExtenstion
    {
        public static void AddDepndencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new ReviewProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
