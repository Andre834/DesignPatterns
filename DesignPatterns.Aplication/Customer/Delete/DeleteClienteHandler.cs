
using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;
using DesignPatterns.DataBase.DataBase;

namespace DesignPatterns.Aplication;


public sealed record DeleteClienteHandler : IHandler<DeleteClienteRequest>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClienteHandler
    (
        IClienteRepository clienteRepository,
        IUnitOfWork unitOfWork
    )
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(DeleteClienteRequest request)
    {
        _clienteRepository.Delete(request.Id);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
