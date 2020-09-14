using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DTO
{
    public class ClienteDTO
    {
        private int _id;
        private int _idade;
        private string _nome;
        private string _email;
        private int _telefone;
        private DateTime _dataCadastro;
        

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Email { get => _email; set => _email = value; }
        public int Idade { get => _idade; set => _idade = value; }
        public int Telefone { get => _telefone; set => _telefone = value; }

        public DateTime DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
       
    }
}
