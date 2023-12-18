using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Reaccion
    {
        #region Atributos
        private bool _like;
        private Miembro _autor;
        #endregion

        public Reaccion(bool like, Miembro autor) { 
            _like = like;
            _autor = autor;
        }

        public bool Like
        {
            get { return _like; }
            set {  _like = value; }
        }

        public Miembro Autor
        {  
            get { return _autor; } 
        }
    }
}
