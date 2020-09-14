using Agenda.DAO;
using Agenda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Model
{
    public class ClienteModel
    {
        public static int Incluir(ClienteDTO dto)
        {
            return new ClienteDAO().Incluir(dto);
        }

        public List<ClienteDTO> ListarDados()
        {
            return new ClienteDAO().ListarDados();
        }

        public static int EditarUsuario(ClienteDTO dto)
        {
            return new ClienteDAO().EditarUsuario(dto);
        }

        public static int Excluir(ClienteDTO dto)
        {
            return new ClienteDAO().Excluir(dto);
        }

        public List<ClienteDTO> PesquisaRapida(ClienteDTO dto)
        {
            return new ClienteDAO().PesquisaRapida(dto);
        }
    }
}
