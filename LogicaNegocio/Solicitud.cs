using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Solicitud:IValidate,IEquatable<Solicitud>
    {
        #region Atributos
        int _id;
        private Miembro _miembroSolicitante;
        private Miembro _miembroSolicitado;
        private string _estado;
        private DateTime _fecha;
        private static int s_ultimoId = 0;
        #endregion

        public Solicitud(Miembro solicitante, Miembro solicitado, string estado) {
            _miembroSolicitado = solicitado;
            _miembroSolicitante = solicitante;
            _id = s_ultimoId++;
            _estado = estado;
            _fecha = DateTime.Now;
        }

        public Usuario MiembroSolicitante
        {
            get { return _miembroSolicitante; }
        }

        public Usuario MiembroSolicitado
        {
            get { return _miembroSolicitado; }
        }

        public string Estado
        { 
            get { return _estado; }
            set { _estado = value; }
        }

        public int Id
        { 
            get { return _id; } 
            set { _id = value; }
        }



        public bool Equals(Solicitud? other)
        {
            //Si miembroSolicitado y miembroSolicitante son el mismo, es la misma solicitud de amistad
            return _miembroSolicitado == other._miembroSolicitado && _miembroSolicitante == other._miembroSolicitante;
        }

        public void ValidarDatos()
        {
            if (_miembroSolicitado==_miembroSolicitante)
            {
                throw new Exception("No te puedes enviar solicitud a ti mismo");
            }   
        }

        public override string ToString()
        {
            return "El miembro solicitante es " + _miembroSolicitante + " El miembro solicitado es " + _miembroSolicitado;
        }
    }
}
