using Agenda.DAO;
using Agenda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Model
{
    public class UsuarioModel
    {
        public UsuarioDTO VerificarLogin(UsuarioDTO dto)
        {
            return new UsuarioDAO().VerificarLogin(dto);
        }
    }
}
