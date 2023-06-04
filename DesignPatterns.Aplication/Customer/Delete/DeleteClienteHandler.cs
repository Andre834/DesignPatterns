
using DesignPatterns.CrossCutting;
using DesignPatterns.DataBase.Cliente;
using DesignPatterns.DataBase.DataBase;

namespace DesignPatterns.Aplication;


public sealed record DeleteClienteHandler : IHandler<DeleteClienteRequest>
{
    private readonly IClienteRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClienteHandler
    (
        IClienteRepository customerRepository,
        IUnitOfWork unitOfWork
    )
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(DeleteClienteRequest request)
    {
        _customerRepository.Delete(request.Id);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
