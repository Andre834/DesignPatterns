namespace DesignPatterns.CrossCutting;

public interface IMediator
{
    Task<Result> HandleAsync<TRequest>(TRequest request);

    Task<Result<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request);
}
