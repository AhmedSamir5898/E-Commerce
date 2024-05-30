using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Talabat.Apis.Errors;
using Talabat.Apis.Helpers;
using Talabat.Core.IRepository;
using Talabat.Repository;

namespace Talabat.Apis.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services )
        {
            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(ProductProfile));
            services.Configure<ApiBehaviorOptions>(Options =>
            Options.InvalidModelStateResponseFactory = (ActionContext) =>
            {
                var Errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                    .SelectMany(p => p.Value.Errors)
                                                     .Select(E => E.ErrorMessage)
                                                     .ToArray();
                var validationError = new ApiValidationErrorResponse()
                {
                    Errors = Errors
                };
                return new BadRequestObjectResult(validationError);


            }
            );
            return services;
        }
    }
}
