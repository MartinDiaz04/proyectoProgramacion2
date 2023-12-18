using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Comentario:Publicacion,IValidate
    {
   

        public Comentario(string contenido, Miembro autor, string titulo, bool publico):base(contenido, autor, titulo, publico)
        {
        }

        public Comentario() : base() { }

        public void ValidarDatos()
        {
            base.ValidarDatos();
        }

        public override string ToString()
        {
            return "Comentario: " + base.ToString();
        }


    }
}
