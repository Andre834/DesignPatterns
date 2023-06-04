
using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;

namespace DesignPatterns.Aplication;

public sealed record ListClienteHandler : IHandler<ListClienteRequest, ListClienteResponse>
{
    private readonly IClienteFactory _clienteFactory;
    private readonly IClienteRepository _clienteRepository;

    public ListClienteHandler
    (
        IClienteFactory clienteFactory,
        IClienteRepository clienteRepository
    )
    {
        _clienteFactory = clienteFactory;
        _clienteRepository = clienteRepository;
    }

    public async Task<Result<ListClienteResponse>> HandleAsync(ListClienteRequest request)
    {
        var clientes = await _clienteRepository.ListAsync();

        if (!clientes.Any()) return Result<ListClienteResponse>.Success();

        var models = clientes.Select(_clienteFactory.Create);

        var response = new ListClienteResponse(models);

        return Result<ListClienteResponse>.Success(response);
    }
}
