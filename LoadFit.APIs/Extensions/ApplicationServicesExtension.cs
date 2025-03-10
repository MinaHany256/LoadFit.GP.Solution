using LoadFit.APIs.Errors;
using LoadFit.APIs.Helpers;
using LoadFit.Core;
using LoadFit.Core.Repositories.Contract;
using LoadFit.Core.Services.Contract;
using LoadFit.Repository;
using LoadFit.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoadFit.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };

            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return services;

        }



        public static WebApplication UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;

        }


    }
}
