

using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;
using DesignPatterns.DataBase.DataBase;

namespace DesignPatterns.Aplication;

public sealed record UpdateClienteHandler : IHandler<UpdateClienteRequest>
{
    private readonly IClienteFactory _clienteFactory;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClienteHandler
    (
        IClienteFactory clienteFactory,
        IClienteRepository clienteRepository,
        IUnitOfWork unitOfWork
    )
    {
        _clienteFactory = clienteFactory;
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(UpdateClienteRequest request)
    {
        var cliente = _clienteFactory.Create(request);

        _clienteRepository.Update(cliente);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
