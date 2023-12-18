using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Post : Publicacion, IValidate, IEquatable<Post>
    {

        #region Atributos
        private string _imagen;
        private List<Comentario> _comentarios = new List<Comentario>();
        private bool _censurado;

        #endregion

        public Post(string contenido, Miembro autor, string titulo, bool publico, string imagen) : base(contenido, autor, titulo, publico) {
            _imagen = imagen;
            _censurado = false;
        }

        public Post() : base() { }


        public List<Comentario> Comentarios
        {
            get { return _comentarios; }
        }

        public bool Censurado
        { 
            get { return _censurado;} 
            set { _censurado = value; }
        }


        public void ValidarDatos() { 

            base.ValidarDatos();
            //valida extensión de la imagen utilizando los tres caracteres finales del nombre del fichero
            if (_imagen==null || _imagen.Trim().Length<=3)
            {
                throw new Exception("La extensión de la imágen es muy corta");
            }
            string ultimosCaracteres = _imagen.Substring(_imagen.Length-3);
            if (ultimosCaracteres.ToLower().Trim()!="jpg" && ultimosCaracteres.ToLower().Trim() != "png")
            {
                throw new Exception("La extensión de la imágen no es válida");
            }
        }

        public void agregarComentario(Comentario comentario)
        {
            if (_comentarios.Contains(comentario))
            {
                throw new Exception("El comentario ya existe!");
            } else
            {
                _comentarios.Add(comentario);
            }
        }

        public bool Equals(Post? other)
        {
            return base.Id == other.Id;
        }

        public override string ToString()
        {
            return "Post: " + base.ToString();
        }


    }
}
