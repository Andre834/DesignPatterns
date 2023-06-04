namespace DesignPatterns.CrossCutting;

public interface IHandler<in TRequest>
{
    Task<Result> HandleAsync(TRequest request);
}

public interface IHandler<in TRequest, TResponse>
{
    Task<Result<TResponse>> HandleAsync(TRequest request);
}
