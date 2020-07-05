using System.Linq;
using Arnoosha.API.Errors;
using Arnoosha.API.Mappings;
using Arnoosha.API.Middleware;
using Arnoosha.Core.Interfaces;
using Arnoosha.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Arnoosha.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidationError { Errors = errors };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Document", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseApplicationServices(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Document");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}
