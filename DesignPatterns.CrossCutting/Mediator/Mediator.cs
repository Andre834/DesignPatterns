using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DesignPatterns.CrossCutting;

public sealed record Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Result> HandleAsync<TRequest>(TRequest request)
    {
        var validationResult = Validate(request);

        if (validationResult.IsError) return validationResult;

        var handler = _serviceProvider.GetRequiredService<IHandler<TRequest>>();

        return await handler.HandleAsync(request).ConfigureAwait(false);
    }

    public async Task<Result<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request)
    {
        var validationResult = Validate(request);

        if (validationResult.IsError) return Result<TResponse>.Error(validationResult.Message);

        var handler = _serviceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();

        return await handler.HandleAsync(request).ConfigureAwait(false);
    }

    private Result Validate<TRequest>(TRequest request)
    {
        var validator = _serviceProvider.GetService<AbstractValidator<TRequest>>();

        if (validator is null) return Result.Success();

        var validationResult = validator.Validate(request);

        return validationResult.IsValid ? Result.Success() : Result.Error(validationResult.ToString());
    }
}
