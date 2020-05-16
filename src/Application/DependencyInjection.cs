using System.Reflection;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using TagDossier.Application.Common.Behaviors;
using TagDossier.Application.Common.Validators.ExistsValidator;
using TagDossier.Application.Common.Validators.UniqueValidator;
using TagDossier.Domain.Common.Validators.ExistsValidator;

namespace TagDossier.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            // services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehavior<>));

            services.AddScoped<IExistsValidatorProvider, ExistsValidatorProvider>();
            services.AddScoped<IUniqueValidatorProvider, UniqueValidatorProvider>();

            return services;
        }
    }
}