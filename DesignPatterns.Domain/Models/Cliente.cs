using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesignPatterns.Domain.Models
{
    
    public sealed record Cliente
    {
        private Cliente() { }

        public Cliente
        (
        long id,
            Nome name,
            Email email
        )
        {
            Id = id;
            Nome = name;
            Email = email;
        }

        public long Id { get; private set; }

        public Nome Nome { get; private set; }

        public Email Email { get; private set; }
    }
}
