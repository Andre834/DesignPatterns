
using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;

namespace DesignPatterns.Aplication;

public sealed record ListClienteHandler : IHandler<ListClienteRequest, ListClienteResponse>
{
    private readonly IClienteFactory _customerFactory;
    private readonly IClienteRepository _customerRepository;

    public ListClienteHandler
    (
        IClienteFactory customerFactory,
        IClienteRepository customerRepository
    )
    {
        _customerFactory = customerFactory;
        _customerRepository = customerRepository;
    }

    public async Task<Result<ListClienteResponse>> HandleAsync(ListClienteRequest request)
    {
        var customers = await _customerRepository.ListAsync();

        if (!customers.Any()) return Result<ListClienteResponse>.Success();

        var models = customers.Select(_customerFactory.Create);

        var response = new ListClienteResponse(models);

        return Result<ListClienteResponse>.Success(response);
    }
}
