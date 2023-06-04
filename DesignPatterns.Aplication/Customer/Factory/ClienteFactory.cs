

using DesignPatterns.Domain.Models;

namespace DesignPatterns.Aplication;


public sealed record ClienteFactory : IClienteFactory
{
    public Cliente Create(AddClienteRequest request) => new(default, new Nome(request.Nome), new Email(request.Email));

    public Cliente Create(UpdateClienteRequest request) => new(request.Id, new Nome(request.Nome), new Email(request.Email));

    public ClienteModel Create(Cliente customer) => new(customer.Id, customer.Nome.Value, customer.Email.Value);
}
