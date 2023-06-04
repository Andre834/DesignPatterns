using DesignPatterns.DataBase.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.DataBase.Cliente
{
    public sealed class ClienteRepository : Repository<Domain.Models.Cliente>, IClienteRepository
    {
        public ClienteRepository(Context context) : base(context) { }
    }

}
