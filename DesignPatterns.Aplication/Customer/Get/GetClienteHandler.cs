

using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;

namespace DesignPatterns.Aplication;

public sealed record GetClienteHandler : IHandler<GetClienteRequest, GetClienteResponse>
{
    private readonly IClienteFactory _clienteFactory;
    private readonly IClienteRepository _clienteRepository;

    public GetClienteHandler
    (
        IClienteFactory clienteFactory,
        IClienteRepository clienteRepository
    )
    {
        _clienteFactory = clienteFactory;
        _clienteRepository = clienteRepository;
    }

    public async Task<Result<GetClienteResponse>> HandleAsync(GetClienteRequest request)
    {
        var cliente = await _clienteRepository.GetAsync(request.Id);

        if (cliente is null) return Result<GetClienteResponse>.Success();

        var modelCliente = _clienteFactory.Create(cliente);

        var response = new GetClienteResponse(modelCliente);

        return Result<GetClienteResponse>.Success(response);
    }
}
