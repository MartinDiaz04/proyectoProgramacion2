using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio;

namespace LogicaNegocio
{
    public abstract class Publicacion:IValidate,IComparable<Publicacion>
    {
        #region Atributos
        private int _id;
        private string _contenido;
        private DateTime _fecha;
        private Miembro _autor;
        private string _titulo;
        private bool _publico;
        private List<Reaccion> _reacciones = new List<Reaccion>();
        private decimal _fva;
        private static int s_ultimoId = 0;
        private int _likes = 0;
        private int _dislikes = 0;
        #endregion

        public Publicacion(string contenido, Miembro autor, string titulo, bool publico)
        {
            _id = s_ultimoId;
            s_ultimoId++;
            _contenido = contenido;
            _fecha = DateTime.Now;
            _autor = autor;
            _titulo = titulo;
            _publico = publico;
            _fecha = DateTime.Now;
        }

        public Publicacion() { }

        public int Id 
        {
            get { return _id; }
            set { _id = value; }
        }

        public Miembro Miembro
        {
            get { return _autor; }
            set { _autor = value; }
        }

        public DateTime Fecha
        { 
            get { return _fecha; }
            set { _fecha = value; }
        }

        public bool Publico { get => _publico; set => _publico = value; }
        public List<Reaccion> Reacciones { get => _reacciones; set => _reacciones = value; }
        public decimal Fva { get => _fva; set => _fva = value; }
        public int Likes { get => _likes; set => _likes = value; }
        public int Dislikes { get => _dislikes; set => _dislikes = value; }
        public string Contenido { get => _contenido; set => _contenido = value; }

        #region Propiedades 
        //Contenido de la publicación no esté vacío
        public void ValidarDatos()
        {
            if (_titulo==null || _titulo.Trim().Length<=3)
            {
                throw new Exception("El titulo de la publicación tiene que tener mas de 3 caracteres");
            }
            if (_contenido == null || _contenido.Trim().Length <= 3) {
                throw new Exception("El contenido de la publicación tiene que tener mas de 3 caracteres");
            }
        }




// Calcular el valor de aceptacion
// Recorrer lista de reacciones y sumar si es like o dislike
// Verificar si es publico y realizar el calculo
        public decimal CalcularFva()
        {
            //Si fva es != 0 es porque ya se calculó, se resetea para evitar que se sume de nuevo
            if (_fva != 0)
            {
                _fva= 0;
            }

            if (_publico)
            {
                _fva = (_likes * 5) + (_dislikes * -2) + 10;
            }
            else
            {
                _fva = (_likes * 5) + (_dislikes * -2);
            }
            return _fva;
        }


        //Cuenta la cantidad de likes/dislikes de la publicación y lo almacena en el atributo correspondiente
        public int ActualizarLikes()
        {
            if (_reacciones.Count > 0)
            {
                //resetear likes y dislikes para evitar que se sumen de nuevo
                _likes = 0;
                _dislikes = 0;

                foreach(Reaccion reaccion in _reacciones){
                    if (reaccion.Like)
                    {
                        _likes = _likes + 1;
                    }
                    else
                    {
                        _dislikes = _dislikes + 1;
                    }
                }
            }
            return _likes;
        }



        public override string ToString()
        {
            return "Título: "+_titulo +", Contenido: "+ _contenido;
        }

        //Igual a ToString pero con string diferente, utilizado para mostrar toda la info de la publicación
        public string RetornarPublicacion()
        {
            string contenido = _contenido.Substring(0,50);
            return $"Publicación id ${_id}, La fecha de publicación es {_fecha}, El título es {_titulo}, El contenido del post es: {contenido}";
        }

        public int CompareTo(Publicacion publicacion)
        {
            return _titulo.CompareTo(publicacion._titulo);
        }

        public void AgregarReaccion(Reaccion reaccion)
        {
            for(int i = 0; i<Reacciones.Count; i++)
            {
                //De acuerdo a requerimiento las publicaciones solo pueden ser reaccionadas una vez, es decir
                //no se puede poner like y luego sacarlo
                if (Reacciones[i].Autor == reaccion.Autor)
                {
                    throw new Exception("Ya has reaccionado a esta publicación");
                }
            }
            Reacciones.Add(reaccion);
        }
        #endregion
    }
}
