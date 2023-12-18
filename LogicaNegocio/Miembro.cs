using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaNegocio.Usuario;

namespace LogicaNegocio
{
    public class Miembro:Usuario,IValidate,IComparable<Miembro>, IEquatable<Miembro>
    {
        #region Atributos
        private string _nombre;
        private string _apellido;
        private DateTime _fechaNacimiento;
        private List<Miembro> _listaAmigos = new List<Miembro>();
        private bool _bloqueado;
        #endregion

        public Miembro(string email, string clave, string nombre, string apellido, DateTime fechaNacimiento):base(email, clave){
            _nombre = nombre;
            _apellido = apellido;
            _fechaNacimiento = fechaNacimiento;
            //cuando se crea un miembro no tiene sentido que esté bloqueado por defecto
            _bloqueado = false;
        }

        //Constructor vacío para MVC
        public Miembro() : base() { }


        public List<Miembro> Amigos
        {
            get { return _listaAmigos; }
            set { _listaAmigos = value;}
        }

        public bool Bloqueado 
        {
            get { return _bloqueado; }
            set { _bloqueado = value; }
        }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public DateTime FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }


        //Valida cantidad de caracteres de nombre, apellido y también fecha de nacimiento del nuevo miembro
        public void ValidarDatos()
        {
            // Llamar a validar datos de Usuario
            base.ValidarDatos();
            if(Nombre.Length<3)
            {
                throw new Exception("El nombre no puede tener menos de tres caracteres");
            }
            if(Apellido.Length < 3)
            {
                throw new Exception("El apellido no puede tener menos de tres caracteres");
            }
            if(FechaNacimiento >= DateTime.Now)
            {
                throw new Exception("La fecha de nacimiento no es correcta");
            }
        }

        public override string ToString()
        {
            return Apellido + " " + Nombre;

        }

        public int CompareTo(Miembro other)
        {
            return other.Apellido.CompareTo(Apellido);
        }

        public bool Equals(Miembro? other) {
            return base.Email == other.Email;
        }



        //Método polimórfico para variable de sesión
        public override string TipoDeUsuario()
        {
            return "MIEMBRO";
        }
    }
}
