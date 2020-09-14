using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DTO
{
    public class UsuarioDTO
    {
        private int _id;
        private string _nome;
        private string _login;
        private string _senha;


        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Login { get => _login; set => _login = value; }


     

        public string Senha { get => _senha; set => _senha = value; }
    }
}
