using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Usuario:IValidate,IEquatable<Usuario>
    {
        #region Atributos 
        private string _email;
        private string _clave;
        #endregion

        public Usuario(string email, string clave) { 
            _email = email;
            _clave = clave;
        }

        //Constructor vacío para MVC
        public Usuario() { }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        public bool Equals(Usuario? other)
        {
            return _email.Trim().ToLower() == other.Email.Trim().ToLower();
        }



        public void ValidarDatos()
        {
            if (_email==null || _email.Trim().Length<=3)
            {
                throw new Exception("Email incorrecto");
            }
            if (_clave == null || _clave.Trim().Length <= 3)
            {
                throw new Exception("Contraseña incorrecta");
            }
        }


        public override string ToString()
        {
            return "Este usuario es " + _email;

        }

        //método polimórfico para obtener tipo de usuario en MVC, nunca debería ser "USUARIO" pero se comprueba por robustez
        public virtual string TipoDeUsuario()
        {
            return "USUARIO";
        }
    }
    
}
