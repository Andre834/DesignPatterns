using DesignPatterns.CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace DesignPatternsAPI;

public static class Extensions
{
    public static void AddJsonOptions(this IMvcBuilder builder) => builder.AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

    public static IActionResult DeleteResult(this Task<Result> result) => result.DefaultResult();

    public static IActionResult GetResult<T>(this Task<Result<T>> result) => result.Result.IsError ? new BadRequestObjectResult(result.Result.Message) : result.Result.HasValue ? new OkObjectResult(result.Result.Value) : new NoContentResult();

    public static IActionResult PatchResult(this Task<Result> result) => result.DefaultResult();

    public static IActionResult PostResult<T>(this Task<Result<T>> result) => result.Result.IsError ? new BadRequestObjectResult(result.Result.Message) : new OkObjectResult(result.Result.Value);

    public static IActionResult PutResult(this Task<Result> result) => result.DefaultResult();

    public static void UseException(this IApplicationBuilder application)
    {
        var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (environment.IsDevelopment())
        {
            application.UseDeveloperExceptionPage();
        }
        else
        {
            application.UseExceptionHandler(builder => builder.Run(async context => await context.Response.WriteAsync(string.Empty)));
        }
    }

    public static void UseLocalization(this IApplicationBuilder application)
    {
        var cultures = new[] { "en", "pt" };

        application.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures(cultures);
            options.AddSupportedUICultures(cultures);
            options.SetDefaultCulture(cultures.First());
        });
    }

    private static IActionResult DefaultResult(this Task<Result> result) => result.Result.IsError ? new BadRequestObjectResult(result.Result.Message) : new OkResult();
}
