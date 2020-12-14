using ConfigurationProvider;
using Microsoft.Extensions.DependencyInjection;
using System;
using TAL.Domain.Interfaces;
using TAL.Service.Interfaces;
using TAL.Services;

namespace TAL.DependencyInjection
{
    public class Register
    {
        public static void RegisterTypes(IServiceCollection container)
        {
            container.AddScoped<IProvideTALData, TALDataProvider>();
            container.AddScoped<ICalculatePremium, CalculatePremium>();
            container.AddScoped<IGetOccupations, Occupations>();

        }
    }
}
