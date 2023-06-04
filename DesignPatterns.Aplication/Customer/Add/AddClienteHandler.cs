

using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;
using DesignPatterns.DataBase.DataBase;

namespace DesignPatterns.Aplication;

public sealed record AddClienteHandler : IHandler<AddClienteRequest, AddClienteResponse>
{
    private readonly IClienteFactory _clienteFactory;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddClienteHandler
    (
        IClienteFactory clienteFactory,
        IClienteRepository customerRepository,
        IUnitOfWork unitOfWork
    )
    {
        _clienteFactory = clienteFactory;
        _clienteRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddClienteResponse>> HandleAsync(AddClienteRequest request)
    {
        var cliente = _clienteFactory.Create(request);

        _clienteRepository.Add(cliente);

        await _unitOfWork.SaveChangesAsync();

        var response = new AddClienteResponse(cliente.Id);

        return Result<AddClienteResponse>.Success(response);
    }
}
