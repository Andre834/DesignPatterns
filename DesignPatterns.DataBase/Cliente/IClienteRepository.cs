using DesignPatterns.DataBase.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.DataBase.Cliente
{
    public interface IClienteRepository : IRepository<Domain.Models.Cliente> { }
}
