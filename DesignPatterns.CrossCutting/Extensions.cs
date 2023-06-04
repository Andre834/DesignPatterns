using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Reflection;

namespace DesignPatterns.CrossCutting;

public static class Extensions
{
    public static void AddAppSettings<T>(this IServiceCollection services, string section) where T : class, new()
    {
        var instance = Activator.CreateInstance(typeof(T));

        new ConfigurationBuilder().Configuration().GetSection(section).Bind(instance);

        services.AddSingleton((T)instance);
    }

    public static void AddClassesMatchingInterfaces(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(Assemblies()).AddClasses().AsMatchingInterface());
    }

    public static void AddJsonStringLocalizer(this IServiceCollection services)
    {
        services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
    }

    public static void AddMediator(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();

        services.AddHandlers();

        services.AddValidators();
    }

    public static IConfigurationRoot Configuration(this ConfigurationBuilder configuration)
    {
        return configuration.AddJsonFile("AppSettings.json", false, true).AddEnvironmentVariables().Build();
    }

    public static void Serilog(this IHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder().Configuration();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        builder.UseSerilog();
    }

    private static void AddHandlers(this IServiceCollection services)
    {
        static bool Handler(Type type) => type.Is(typeof(IHandler<>)) || type.Is(typeof(IHandler<,>));

        Types().Where(type => type.GetInterfaces().Any(Handler)).ToList().ForEach(type => type.GetInterfaces().Where(Handler).ToList().ForEach(@interface => services.AddScoped(@interface, type)));
    }

    private static void AddValidators(this IServiceCollection services)
    {
        Types().Where(type => type.BaseType.Is(typeof(AbstractValidator<>))).ToList().ForEach(type => services.AddSingleton(type.BaseType, type));
    }

    private static IEnumerable<Assembly> Assemblies()
    {
        return DependencyContext.Default.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(nameof(DesignPatterns))).Select(Assembly.Load);
    }

    private static bool Is(this Type type, MemberInfo memberInfo)
    {
        return type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == memberInfo;
    }

    private static IEnumerable<Type> Types()
    {
        return Assemblies().SelectMany(assembly => assembly.GetTypes());
    }
}
