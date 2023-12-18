using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Administrador:Usuario, IValidate
    {
        public Administrador(string email, string clave):base(email, clave) {}

        //método polimórfico para variable de sesión
        public override string TipoDeUsuario()
        {
            return "ADMIN";
        }
    }
}
