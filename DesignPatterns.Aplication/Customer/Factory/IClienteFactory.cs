

using DesignPatterns.Domain.Models;

namespace DesignPatterns.Aplication;


public interface IClienteFactory
{
    Cliente Create(AddClienteRequest request);

    Cliente Create(UpdateClienteRequest request);

    ClienteModel Create(Cliente cliente);
}
