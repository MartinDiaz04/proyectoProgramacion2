using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Sistema
    {
        #region Atributos
        public List<Usuario> _listaUsuarios = new List<Usuario>();
        public List<Publicacion> _listaPublicaciones = new List<Publicacion>();
        public List<Solicitud> _listaSolicitudes = new List<Solicitud>();
        private static Sistema _instancia;
        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Sistema();
                }
                return _instancia;
            }
        }
        #endregion

        public Sistema()
        {
            Precarga();
        }


        public void Precarga()
        {
            PrecargarUsuarios();
            PreCargarPublicaciones();
            PreCargarSolicitudes();
        }


        // Acceso a las listas
        public List<Usuario> Usuarios
        {
            get { return _listaUsuarios; }
        }
        public List<Publicacion> Publicaciones
        {
            get { return _listaPublicaciones; }
        }

        public void PrecargarUsuarios()
        {
            // Miembro
            AltaMiembro(new Miembro("martin@gmail.com", "hola123", "Martin", "Diaz", new DateTime(22 / 04 / 2004)));
            AltaMiembro(new Miembro("santiago@gmail.com", "santiago123", "Santiago", "Pintos", new DateTime(01 / 04 / 1997)));
            AltaMiembro(new Miembro("pedroRodriguez@gmail.com", "pedrito", "Pedro", "Rodriguez", new DateTime(11 / 09 / 2001)));
            AltaMiembro(new Miembro("sandraPerez@gmail.com", "sandaP", "Sandra", "Perez", new DateTime(26 / 12 / 2006)));
            AltaMiembro(new Miembro("juanLopez@gmail.com", "juan1", "Juan", "Lopez", new DateTime(15 / 10 / 1970)));
            AltaMiembro(new Miembro("joseLuis@gmail.com", "josesito", "Jose Luis", "Sanchez", new DateTime(26 / 08 / 1971)));
            AltaMiembro(new Miembro("lauraMoreno@gmail.com", "laurita123", "Laura", "Moreno", new DateTime(12 / 06 / 2011)));
            AltaMiembro(new Miembro("leonardo@gmail.com", "leonardoPablo", "Leonardo", "Aguirre", new DateTime(23 / 02 / 2002)));
            AltaMiembro(new Miembro("paulaD@gmail.com", "paula321", "Paula", "Fernandez", new DateTime(1 / 01 / 2001)));
            AltaMiembro(new Miembro("carlos@gmail.com", "carlote", "Carlos", "Preiero", new DateTime(29 / 07 / 1980)));

            // Administradores
            AltaAdministrador(new Administrador("santiaguito@gmail.com", "santiago"));
        }

        public void PreCargarPublicaciones()
        {
            Miembro martin = (Miembro)BuscarUsuario("martin@gmail.com");
            Miembro pedro = (Miembro)BuscarUsuario("pedroRodriguez@gmail.com");
            Miembro leonardo = (Miembro)BuscarUsuario("leonardo@gmail.com");
            Miembro laura = (Miembro)BuscarUsuario("lauraMoreno@gmail.com");
            Miembro sandra = (Miembro)BuscarUsuario("sandraPerez@gmail.com");
            Miembro juan = (Miembro)BuscarUsuario("juanLopez@gmail.com");
            Miembro jose = (Miembro)BuscarUsuario("joseLuis@gmail.com");
            Miembro paula = (Miembro)BuscarUsuario("paulaD@gmail.com");
            Miembro carlos = (Miembro)BuscarUsuario("carlos@gmail.com");
            Miembro santiago = (Miembro)BuscarUsuario("santiago@gmail.com");



            // Precargo 5 publicaciones
            NuevoPost("gatito feliz", martin, "Un gato", true, "gato.jpg");
            NuevoPost("imagen de un perro", pedro, "Un perrito", true, "unperrito.jpg");
            NuevoPost("imagen del sol", leonardo, "El sol", true, "elsol.jpg");




            NuevoPost("auto volador", laura, "El nuevo auto volador", true, "auto.png");
            Post postNuevo = (Post)BuscarPost(3);
            ReaccionarPublicacion(postNuevo, new Reaccion(true, pedro));


            NuevoPost("caballo en el campo", sandra, "Un caballo pastando", false, "caballito.jpg");
            Post postNuevo2 = (Post)BuscarPost(4);
            ReaccionarPublicacion(postNuevo2, new Reaccion(true, martin));


            // Precargo 3 comentarios por cada publicacion

            // Publicacion 1

            NuevoComentario("Lindo gato", santiago, "gato", (Post)BuscarPost(0));
            Comentario nuevoComentario = BuscarComentario(5);
            ReaccionarPublicacion(nuevoComentario, new Reaccion(true, carlos));
            NuevoComentario("Que hermoso ese gato", juan, "gatito hermoso", (Post)BuscarPost(0));
            NuevoComentario("Que buen animal", jose, "gran animal", (Post)BuscarPost(0));

            // Publicacion 2
            NuevoComentario("Lindo gato", paula, "gato", (Post)BuscarPost(1));
            Comentario nuevoComentario1 = BuscarComentario(8);
            ReaccionarPublicacion(nuevoComentario1, new Reaccion(true, carlos));

            NuevoComentario("Precioso perro", carlos, "precioso perro", (Post)BuscarPost(1));
            Comentario nuevoComentario2 = BuscarComentario(9);
            ReaccionarPublicacion(nuevoComentario2, new Reaccion(true, pedro));


            NuevoComentario("Precioso el canino", leonardo, "precioso perro", (Post)BuscarPost(1));
            NuevoComentario("Divino el caniche", sandra, "caniche", (Post)BuscarPost(1));

            // Publicacion 3
            NuevoComentario("Lindo atardecer", santiago, "lindo atardecer", (Post)BuscarPost(2));
            NuevoComentario("Gran sol", martin, "gran sol", (Post)BuscarPost(2));
            NuevoComentario("Buen dia soleado", laura, "buen dia", (Post)BuscarPost(2));

            // Publicacion 4
            NuevoComentario("Que tecnologia", paula, "tecnologico", (Post)BuscarPost(3));
            NuevoComentario("Que hermoso auto", leonardo, "auto interesante", (Post)BuscarPost(3));
            NuevoComentario("Buen auto volador", jose, "auto volador", (Post)BuscarPost(3));


            // Publicacion 5
            NuevoComentario("Esta grande el caballo", martin, "caballo grande", (Post)BuscarPost(4));
            NuevoComentario("Como come ese caballo", santiago, "caballo come", (Post)BuscarPost(4));
            NuevoComentario("Que lindo pelaje", juan, "lindos pelos", (Post)BuscarPost(4));

            ActualizarLikesYfva();
        }


        public void PreCargarSolicitudes()
        {
            // VERIFICAR PRECARGA PARA NO TOMAR MIEMBROS CON INDICE

            // Solicitudes primer usuario
            Solicitud solicitud1 = EnviarSolicitud((Miembro)BuscarUsuario("paulaD@gmail.com"), (Miembro)BuscarUsuario("carlos@gmail.com"));
            Solicitud solicitud2 = EnviarSolicitud((Miembro)BuscarUsuario("sandraPerez@gmail.com"), (Miembro)BuscarUsuario("carlos@gmail.com"));
            AceptarSolicitud(solicitud1);
            AceptarSolicitud(solicitud2);



            // Solicitudes segundo usuario
            EnviarSolicitud((Miembro)BuscarUsuario("sandraPerez@gmail.com"), (Miembro)BuscarUsuario("martin@gmail.com"));
            Solicitud solicitud4 = EnviarSolicitud((Miembro)BuscarUsuario("leonardo@gmail.com"), (Miembro)BuscarUsuario("martin@gmail.com"));
            RechazarSolicitud(solicitud4);





            // Solicitudes tercer usuario
            EnviarSolicitud((Miembro)BuscarUsuario("pedroRodriguez@gmail.com"), (Miembro)BuscarUsuario("santiago@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("joseLuis@gmail.com"), (Miembro)BuscarUsuario("santiago@gmail.com"));
            // Solicitudes cuarto usuario
            EnviarSolicitud((Miembro)BuscarUsuario("lauraMoreno@gmail.com"), (Miembro)BuscarUsuario("pedroRodriguez@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("leonardo@gmail.com"), (Miembro)BuscarUsuario("pedroRodriguez@gmail.com"));
            // Solicitudes quinto usuario
            EnviarSolicitud((Miembro)BuscarUsuario("paulaD@gmail.com"), (Miembro)BuscarUsuario("sandraPerez@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("juanLopez@gmail.com"), (Miembro)BuscarUsuario("sandraPerez@gmail.com"));
            // Solicitudes sexto usuario
            EnviarSolicitud((Miembro)BuscarUsuario("carlos@gmail.com"), (Miembro)BuscarUsuario("juanLopez@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("joseLuis@gmail.com"), (Miembro)BuscarUsuario("juanLopez@gmail.com"));
            // Solicitudes septimo usuario
            EnviarSolicitud((Miembro)BuscarUsuario("lauraMoreno@gmail.com"), (Miembro)BuscarUsuario("joseLuis@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("paulaD@gmail.com"), (Miembro)BuscarUsuario("joseLuis@gmail.com"));
            // Solicitudes octavo usuario
            EnviarSolicitud((Miembro)BuscarUsuario("santiago@gmail.com"), (Miembro)BuscarUsuario("lauraMoreno@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("carlos@gmail.com"), (Miembro)BuscarUsuario("lauraMoreno@gmail.com"));
            // Solicitudes noveno usuario
            EnviarSolicitud((Miembro)BuscarUsuario("sandraPerez@gmail.com"), (Miembro)BuscarUsuario("leonardo@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("paulaD@gmail.com"), (Miembro)BuscarUsuario("leonardo@gmail.com"));
            // Solicitudes decimo usuario
            EnviarSolicitud((Miembro)BuscarUsuario("lauraMoreno@gmail.com"), (Miembro)BuscarUsuario("paulaD@gmail.com"));
            EnviarSolicitud((Miembro)BuscarUsuario("juanLopez@gmail.com"), (Miembro)BuscarUsuario("paulaD@gmail.com"));
        }








        public void AltaMiembro(Miembro nuevoMiembro)
        {
            nuevoMiembro.ValidarDatos();
            if (!_listaUsuarios.Contains(nuevoMiembro))
            {
                _listaUsuarios.Add(nuevoMiembro);
            }
            else
            {
                throw new Exception("Ya existe un miembro con el mismo email");
            }
        }


        public void AltaAdministrador(Administrador admin)
        {
            admin.ValidarDatos();
            if (!_listaUsuarios.Contains(admin))
            {
                _listaUsuarios.Add(admin);
            }
        }

        //De acuerdo al ejemplo de la profesora (Club deportivo) se debe recibir el miembro al que se le agrega el post

        public void NuevoPost(string contenido, Miembro autor, string titulo, bool publico, string imagen)
        {
            Post nuevoPost = new Post(contenido, autor, titulo, publico, imagen);
            nuevoPost.ValidarDatos();
            _listaPublicaciones.Add(nuevoPost);
        }

        public void NuevoComentario(string contenido, Miembro miembro, string titulo, Post post)
        {
            Comentario nuevoComentario = new Comentario(contenido, miembro, titulo, post.Publico);
            try
            {
                nuevoComentario.ValidarDatos();
                post.agregarComentario(nuevoComentario);
                _listaPublicaciones.Add(nuevoComentario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // DEVUELVE UNA SOLICITUD PORQUE SINO LA PRECARGA TIRA ERROR
        public Solicitud EnviarSolicitud(Miembro miembroSolicitante, Miembro miembroSolicitado)
        {
            Solicitud nuevaSolicitud = null;
            // verificar que los usuarios no sean nulos
            if (miembroSolicitado != null && miembroSolicitado != null)
            {
                // crear la solicitud en pendiente
                nuevaSolicitud = new Solicitud(miembroSolicitante, miembroSolicitado, "PENDIENTE_APROBACION");
                nuevaSolicitud.ValidarDatos();

                // verificar si esta bloqueado
                UsuarioEstaBloqueado(miembroSolicitante);

                // RECORRER LA LISTA DE SOLICITUDES Y VERIFICAR SI YA SON AMIGOS
                for (int n = 0; n < _listaSolicitudes.Count; n++)
                {
                    if (_listaSolicitudes[n].MiembroSolicitado == miembroSolicitado && _listaSolicitudes[n].MiembroSolicitante == miembroSolicitante && _listaSolicitudes[n].Estado == "APROBADO")
                    {
                        throw new Exception("Ya es tu amigo");
                    }
                }

                // AGREGAR LA SOLICITUD A LA LISTA DDE SOLICITUDES
                // EMPIEZA EN -1 PORQUE SINO NUNCA ENTRA EN LA PRIMER PRECARGA
                for (int n = -1; n < _listaSolicitudes.Count; n++)
                {
                    if (!_listaSolicitudes.Contains(nuevaSolicitud))
                    {
                        _listaSolicitudes.Add(nuevaSolicitud);
                    }
                }
            }
            else
            {
                throw new Exception("Uno de los usuarios es nulo");
            }
            return nuevaSolicitud;
        }


        // METODO PARA DEVOLVER LAS PUBLICACIONES DE UN USUARIO
        public List<Publicacion> DevolverPublicaciones(string email)
        {
            List<Publicacion> publicaciones = new List<Publicacion>();

            for (int n = 0; n < _listaPublicaciones.Count; n++)
            {
                if (email.Trim().ToLower() == _listaPublicaciones[n].Miembro.Email.ToLower())
                {
                    publicaciones.Add(_listaPublicaciones[n]);
                }
            }
            return publicaciones;
        }



        public List<Comentario> DevolverComentariosDeUnUsuario(string email)
        {
            List<Comentario> comentarios = new List<Comentario>();

            Miembro miembroBuscado = (Miembro)BuscarUsuario(email);

            if (miembroBuscado != null)
            {
                for (int n = 0; n < _listaPublicaciones.Count; n++)
                {
                    if (_listaPublicaciones[n] is Post)
                    {
                        Post postActual = (Post)_listaPublicaciones[n];
                        if (postActual.Comentarios.Count > 0)
                        {
                            for (int i = 0; i < postActual.Comentarios.Count; i++)
                            {
                                if (postActual.Comentarios[i].Miembro.Email == miembroBuscado.Email)
                                {
                                    comentarios.Add(comentarios[i]);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception("No se encontro el usuario");
            }

            return comentarios;
        }




        // CORRECCION DE ESTE METODO
        // EN LUGAR DE RECORRER LISTA DE USUARIOS Y VER EL COUNT DE SUS PUBLICACIONES, LLAMAR AL METODO QUE DEVUELVE LA LISTA DE PUBLICACIONES DE UN USUARIO Y VERIFICAR EL COUNT
        public List<Miembro> DevolverUsuariosConMasPublicaciones()
        {
            List<Miembro> listaMiembro = new List<Miembro>();
            int maximaCanPublicaciones = int.MinValue;

            // Verificar si hay algun miembro con la misma cantidad de publicaciones
            for (int n = 0; n < _listaUsuarios.Count; n++)
            {
                if (_listaUsuarios[n] is Miembro)
                {
                    Miembro miembroActual = (Miembro)_listaUsuarios[n];
                    // Obtener publicaciones de un usuario
                    List<Publicacion> listaPublicacionesUsuario = DevolverPublicaciones(miembroActual.Email);
                    // Verificar el largo de esa lista de publicaciones
                    if (listaPublicacionesUsuario.Count == maximaCanPublicaciones)
                    {
                        listaMiembro.Add(miembroActual);
                    }
                    if (listaPublicacionesUsuario.Count > maximaCanPublicaciones)
                    {
                        listaMiembro.Clear();
                        listaMiembro.Add(miembroActual);
                        maximaCanPublicaciones = listaPublicacionesUsuario.Count;
                    }


                }
            }
            return listaMiembro;
        }



        // DEVOLVER LAS SOLICITUDES DE UN USUARIO POR EL EMAIL
        public List<Solicitud> DevolverSolicitudesUsuario(string email)
        {
            List<Solicitud> solicitudes = new List<Solicitud>();

            for (int n = 0; n < _listaSolicitudes.Count; n++)
            {
                if (email.Trim().ToLower() == _listaSolicitudes[n].MiembroSolicitado.Email.Trim().ToLower())
                {
                    solicitudes.Add(_listaSolicitudes[n]);
                }
            }
            return solicitudes;
        }

        public List<Solicitud> RetornarSolicitudesPendientes(string email)
        {
            List<Solicitud> listaCompletaSolicitudes = DevolverSolicitudesUsuario(email);
            List<Solicitud> listaSolicitudes = new List<Solicitud>();
            // LLAMAR AL METODO DEVOLVER SOLICITUDES DE UN USUARIO Y VERIFICAR SU ESTADO
            for (int n = 0; n < listaCompletaSolicitudes.Count; n++)
            {
                if (listaCompletaSolicitudes[n].Estado.ToUpper() == "PENDIENTE_APROBACION")
                {
                    listaSolicitudes.Add(listaCompletaSolicitudes[n]);
                }
            }
            return listaSolicitudes;
        }

        public Solicitud ObtenerSolicitud(Solicitud solicitud)
        {
            Solicitud encontrada = null;
            for (int i = 0; i < _listaSolicitudes.Count && encontrada == null; i++)
            {
                if (_listaSolicitudes[i] == solicitud)
                {
                    encontrada = _listaSolicitudes[i];
                }
            }
            if (encontrada == null)
            {
                //esto no debería pasar, pero se comprueba por robustez
                throw new Exception("Error obteniendo la solicitud de amistad");
            }

            return encontrada;
        }
        public void AceptarSolicitud(Solicitud solicitud)
        {
            Solicitud solicitudActual = ObtenerSolicitud(solicitud);
            solicitudActual.Estado = "APROBADO";
        }
        public void RechazarSolicitud(Solicitud solicitud)
        {
            Solicitud solicitudActual = ObtenerSolicitud(solicitud);
            solicitudActual.Estado = "RECHAZADO";
        }


        public Usuario BuscarUsuario(string email)
        {
            Usuario usuarioBuscado = null;
            bool encontrado = false;
            if (email != null)
            {
                for (int n = 0; n < _listaUsuarios.Count && !encontrado; n++)
                {
                    if (_listaUsuarios[n].Email.Trim().ToLower() == email.ToLower().Trim())
                    {
                        usuarioBuscado = (Usuario)_listaUsuarios[n];
                        encontrado = true;
                    }
                }
            }
            return usuarioBuscado;
        }



        //Método auxiliar usado para saber si un usuario se encuentra bloqueado antes de realizar ciertas acciones
        public void UsuarioEstaBloqueado(Miembro usuario)
        {
            if (usuario.Bloqueado)
            {
                throw new Exception("Esta función se encuentra deshabilitada porque el usuario está bloqueado");
            }
        }


        //Obtiene publicaciones en un rango de fechas
        public List<Publicacion> ObtenerPublicacionesEntreDosFechas(DateTime primeraFecha, DateTime segundaFecha)
        {
            List<Publicacion> listaPublicaciones = new List<Publicacion>();
            //primerafecha debería ser anterior a segunda fecha
            int comparacion = DateTime.Compare(primeraFecha, segundaFecha);
            //DateTime.Compare retorna -1 si primeraFecha < segundaFecha, 0 si primeraFecha = segundaFecha
            //y 1 si primeraFecha>segundaFecha
            if (comparacion == 0)
            {
                throw new Exception("Las fechas ingresadas son iguales");
            }
            //si primeraFecha es posterior a segundaFecha se invierten los valores para no romper lógica
            if (comparacion == 1)
            {
                (primeraFecha, segundaFecha) = (segundaFecha, primeraFecha);
            }
            for (int i = 0; i < _listaPublicaciones.Count; i++)
            {
                if (_listaPublicaciones[i] is Post)
                {
                    Post postActual = (Post)_listaPublicaciones[i];
                    if (postActual.Fecha >= primeraFecha && postActual.Fecha <= segundaFecha)
                    {
                        listaPublicaciones.Add(postActual);
                    }
                }
            }
            return listaPublicaciones;
        }


        public List<Publicacion> OrdenarLista(List<Publicacion> publicaciones)
        {
            publicaciones.Sort();
            return publicaciones;
        }

        public string RetornarPublicacion(Publicacion publicacion)
        {
            return publicacion.RetornarPublicacion();
        }

        public void ReaccionarPublicacion(Publicacion publicacion, Reaccion reaccion)
        {
            try
            {
                publicacion.AgregarReaccion(reaccion);
                //se actualizan los likes y dislikes de la publicación para que se muestren datos actualizados en la vista
                ActualizarLikesYfva();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public Publicacion BuscarPost(int id)
        {
            Publicacion postEncontrado = null;
            for (int n = 0; n < _listaPublicaciones.Count && postEncontrado == null; n++)
            {
                if (id == _listaPublicaciones[n].Id)
                {
                    postEncontrado = _listaPublicaciones[n];
                }
            }
            return postEncontrado;
        }

        public Comentario BuscarComentario(int id)
        {
            Comentario comentarioEncontrado = null;
            for (int n = 0; n < _listaPublicaciones.Count && comentarioEncontrado == null; n++)
            {
                if (id == _listaPublicaciones[n].Id)
                {
                    if (_listaPublicaciones[n] is Comentario)
                    {
                        comentarioEncontrado = (Comentario)_listaPublicaciones[n];
                    }
                }
            }
            return comentarioEncontrado;
        }


        //retorna lista de usuarios tipo Miembro ordenados por apellido de forma ascendente
        public List<Miembro> ObtenerListaDeMiembrosOrdenadosPorApellido()
        {
            List<Miembro> listaDeMiembros = new List<Miembro>();
            for (int i = 0; i < _listaUsuarios.Count; i++)
            {
                if (_listaUsuarios[i] is Miembro)
                {
                    listaDeMiembros.Add((Miembro)_listaUsuarios[i]);
                }
            }
            // si la lista está vacía no tiene sentido intentar ordenarla
            if (listaDeMiembros.Count > 0)
            {
                listaDeMiembros.Sort();
            }

            return listaDeMiembros;
        }

        //retorna lista de Miembro NO bloqueados, es usado desde controlador de admin para bloquear usuarios
        public List<Miembro> ObtenerMiembrosNoBloqueados()
        {
            List<Miembro> usuarios = new List<Miembro>();

            for (int i = 0; i < _listaUsuarios.Count; i++)
            {
                if (_listaUsuarios[i] is Miembro)
                {
                    Miembro miembro = (Miembro)_listaUsuarios[i];
                    if (!miembro.Bloqueado)
                    {
                        usuarios.Add(miembro);
                    }
                }
            }

            return usuarios;
        }

        //método usado por controlador de adminsitrador para bloquear usuarios de tipo miembro
        public void BloquearUsuario(Miembro miembro)
        {
            if (miembro != null)
            {
                Miembro miembroBuscado = (Miembro)BuscarUsuario(miembro.Email);
                //si correos de ambos son iguales, es el mismo usuario
                if (miembroBuscado.Equals(miembro))
                {
                    miembro.Bloqueado = true;
                }
            }
            else
            {
                //no debería pasar, pero se comprueba por robustez
                throw new Exception("El usuario recibido es nulo");
            }
        }

        public List<Post> ObtenerListaDePosts()
        {
            List<Post> lista = new List<Post>();
            for (int i = 0; i < _listaPublicaciones.Count; i++)
            {
                if (_listaPublicaciones[i] is Post)
                {
                    lista.Add((Post)Publicaciones[i]);
                }
            }
            return lista;
        }

        public void BanearPost(Post post)
        {
            if (post != null)
            {
                for (int i = 0; i < Publicaciones.Count; i++)
                {
                    if (Publicaciones[i] is Post)
                    {
                        if (Publicaciones[i].Equals(post))
                        {
                            Post postBuscado = (Post)Publicaciones[i];
                            postBuscado.Censurado = true;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("El Post recibido por BanearPost es nulo");
            }
        }


        public List<Post> DevolverPostDeUsuarioYamigos(string email)
        {
            List<Post> listaPublicaciones = new List<Post>();
            Miembro miembro = (Miembro)BuscarUsuario(email);
            List<Miembro> amigosMiembro = miembro.Amigos;
            // Agrega los publicos primero y luego verifica si es de amigo o de el mismo para agregarlos
            listaPublicaciones = ObtenerPostPublicos();
            for (int n = 0; n < _listaPublicaciones.Count; n++)
            {
                // CHEQUEAR EL CENSURADO DE POST
                if (_listaPublicaciones[n] is Post)
                {
                    if (amigosMiembro.Contains(_listaPublicaciones[n].Miembro) || miembro.Equals(_listaPublicaciones[n].Miembro) && !listaPublicaciones.Contains(_listaPublicaciones[n]))
                    {
                        listaPublicaciones.Add((Post)_listaPublicaciones[n]);
                    }
                }
            }
            return listaPublicaciones;
        }



        public List<Post> ObtenerPostPublicos()
        {
            List<Post> listaPublicaciones = new List<Post>();
            for (int n = 0; n < _listaPublicaciones.Count; n++)
            {
                if (_listaPublicaciones[n] is Post)
                {
                    if (_listaPublicaciones[n].Publico && !listaPublicaciones.Contains(_listaPublicaciones[n]))
                    {
                        listaPublicaciones.Add((Post)_listaPublicaciones[n]);
                    }
                }
            }
            return listaPublicaciones;
        }


        //método usado en MiembroController que retona los usuarios que NO son amigos del recibido por parámetro ni tienen solicitudes de amistad pendientes entre si
        public List<Miembro> DevolverMiembrosNoAmigos(string email)
        {
            List<Miembro> listaNoAmigos = new List<Miembro>();
            Miembro miembro = (Miembro)BuscarUsuario(email);
            List<Miembro> amigosMiembro = miembro.Amigos;
            for (int n = 0; n < _listaUsuarios.Count; n++)
            {
                if (_listaUsuarios[n] is Miembro)
                {
                    //si no es amigo, no es el mismo usuario y tampoco existe solicitud entre ambos se agrega a la lista
                    if (!amigosMiembro.Contains(_listaUsuarios[n]) && !miembro.Equals(_listaUsuarios[n]) && !ExisteSolicitud(miembro, (Miembro)_listaUsuarios[n]))
                    {
                        listaNoAmigos.Add((Miembro)_listaUsuarios[n]);
                    }
                }
            }
            return listaNoAmigos;
        }

        //método que comprueba la NO existencia de solicitudes pendientes entre dos usuarios
        public bool ExisteSolicitud(Miembro miembroSolicitado, Miembro miembroSolicitante)
        {
            bool existeSolicitud = false;
            for (int n = 0; n < _listaSolicitudes.Count && !existeSolicitud; n++)
            {
                if (_listaSolicitudes[n].MiembroSolicitado == miembroSolicitado && _listaSolicitudes[n].MiembroSolicitante == miembroSolicitante || _listaSolicitudes[n].MiembroSolicitado == miembroSolicitante && _listaSolicitudes[n].MiembroSolicitante == miembroSolicitado)
                {
                    existeSolicitud = true;
                }
            }
            return existeSolicitud;
        }

        //busca solicitud por id
        public Solicitud BuscarSolicitud(int id)
        {
            Solicitud solicitud = null;
            for (int n = 0; n < _listaSolicitudes.Count; n++)
            {
                if (_listaSolicitudes[n].Id == id)
                {
                    solicitud = _listaSolicitudes[n];
                }
            }
            return solicitud;
        }


        // Método usado para contar likes de publicaciones y calcular fva cuando se cargan los datos y/o se agregan likes/dislikes
        public void ActualizarLikesYfva()
        {
            for (int n = 0; n < _listaPublicaciones.Count; n++)
            {
                if (_listaPublicaciones[n] is Post)
                {
                    Post post = (Post)_listaPublicaciones[n];
                    post.ActualizarLikes();
                    post.CalcularFva();
                    post.Comentarios.ForEach(comentario => comentario.ActualizarLikes());
                    post.Comentarios.ForEach(comentario => comentario.CalcularFva());
                }
            }

        }


        // método para agregar like a publicación, recibe por parámetro el id y el id del usuario que reacciona
        public void AgregarLike(int idPublicacion, string email)
        {
            Publicacion post = BuscarPost(idPublicacion);
            Miembro miembro = (Miembro)BuscarUsuario(email);
            Reaccion reaccion = new Reaccion(true, miembro);
            try
            {
                ReaccionarPublicacion(post, reaccion);
            }
            catch
            {
                throw new Exception("Ya reaccionaste a esta publicación");
            }
        }

        //método para agregar dislike a publicación, recibe por parámetro id y el id del usuario que reacciona
        public void AgregarDislike(int idPublicacion, string email)
        {
            Publicacion post = BuscarPost(idPublicacion);
            Miembro miembro = (Miembro)BuscarUsuario(email);
            Reaccion reaccion = new Reaccion(false, miembro);
            try
            {
                ReaccionarPublicacion(post, reaccion);
            }
            catch
            {
                throw new Exception("Ya reaccionaste a esta publicación");
            }
        }



            public List<Publicacion> DevolverPublicacionesConCondicion(string texto, int aceptacion, Miembro miembro)
            {
                List<Publicacion> publicaciones = new List<Publicacion>();
                List<Miembro> amigosMiembro = miembro.Amigos;
                if (texto == null)
                {
                    throw new Exception("El texto no puede estar vacío");
                }
                if (aceptacion == null)
                {
                    throw new Exception("El valor de aceptación no puede estar vacío");
                }
                if (aceptacion < 0)
                {
                    throw new Exception("El valor de aceptación no puede ser negativo");
                }

                for (int n = 0; n < _listaPublicaciones.Count; n++)
                    {

                        //busca en todas las publicaciones
                        if (_listaPublicaciones[n].Publico && !publicaciones.Contains(_listaPublicaciones[n]) && _listaPublicaciones[n].Contenido.Contains(texto) && _listaPublicaciones[n].Fva > aceptacion)
                        {
                            publicaciones.Add(_listaPublicaciones[n]);
                        }
                        //busca en publicaciones de amigos, en caso de que sean privadas
                        else if (amigosMiembro.Contains(_listaPublicaciones[n].Miembro) && !publicaciones.Contains(_listaPublicaciones[n]) && _listaPublicaciones[n].Contenido.Contains(texto) && _listaPublicaciones[n].Fva > aceptacion)
                        {
                            publicaciones.Add(_listaPublicaciones[n]);
                        }
                    }
                    return publicaciones;
                }
            }
} 
