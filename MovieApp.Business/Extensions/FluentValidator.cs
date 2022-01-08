using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.FluentValidators;
using MovieApp.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Extensions
{
    public static class FluentValidator
    {
        public static void CustomFluentValidator(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationFilter());
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<LoginValidator>();
            });
        }
    }
}
