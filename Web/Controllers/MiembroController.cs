using LogicaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class MiembroController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            //obtiene datos de Miembro logueado utilizando el email
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroActual = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroActual != null)
                {
                    ViewBag.Usuario = miembroActual.Nombre;
                    List<Post> publicaciones = _sistema.DevolverPostDeUsuarioYamigos(miembroActual.Email);
                    ViewBag.ListarPublicaciones = publicaciones;
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string comentario, string titulo, int id, string accion)
        {
            // REPETIR CODIGO para no crear otra vista comentario y repetir exactamente el Index y el indexhtml para mostrar nuevamente las publicaciones
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
               Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));

               if(miembroLogueado != null)
               {
                    ViewBag.Usuario = miembroLogueado.Nombre;
                    List<Post> publicaciones = _sistema.DevolverPostDeUsuarioYamigos(miembroLogueado.Email);
                    ViewBag.ListarPublicaciones = publicaciones;
                    Publicacion post = _sistema.BuscarPost(id);

                    if (accion.Trim().ToLower() == "enviar"){
                       try
                       {
                            _sistema.NuevoComentario(comentario, miembroLogueado, titulo, (Post)post);
                            ViewBag.Mensaje = "Comentario Agregado";
                       }
                       catch (Exception e)
                       {
                            ViewBag.Mensaje = e.Message;
                       }
                    } 
                    else if (accion.Trim().ToLower() == "like")
                    {
                        try
                        {
                            _sistema.AgregarLike(post.Id, miembroLogueado.Email);
                        } 
                        catch (Exception e)
                        {
                            ViewBag.Mensaje = e.Message;
                        }
                
                    } 
                    else if (accion.Trim().ToLower() == "dislike")
                    {
                        try
                        {
                            _sistema.AgregarDislike(post.Id, miembroLogueado.Email);
                        }
                        catch(Exception e){
                            ViewBag.Mensaje = e.Message;
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo realizar la acción solicitada";
                    }
               }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }


        public IActionResult EnviarSolicitud()
        {
           
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    //obtiene lista de Miembros que no son amigos del usuario logueado ni tienen solicitudes pendientes
                    ViewBag.ListaNoAmigos = _sistema.DevolverMiembrosNoAmigos(miembroLogueado.Email);
                }
                else
                {
                    ViewBag.Msj = "No se pudo obtener el usuario logueado";
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }


        [HttpPost]
        public IActionResult EnviarSolicitud(string email)
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                Miembro miembroAEnviarSolicitud = (Miembro)_sistema.BuscarUsuario(email: email);
                if(miembroAEnviarSolicitud != null && miembroLogueado != null){
                    try
                    {
                        _sistema.EnviarSolicitud(miembroLogueado, miembroAEnviarSolicitud);
                        ViewBag.Msj = "Solicitud de amistad enviada con éxito";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Msj = ex.Message;
                    }
                }
                else { 
                    ViewBag.Msj = "Error, no se pudo obtener a los usuarios para procesar la solicitud";
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        public IActionResult AceptarRechazarSolicitudes()
        {

            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    ViewBag.ListaSolicitudes = _sistema.RetornarSolicitudesPendientes(miembroLogueado.Email);
                }
                else
                {
                    ViewBag.Msj = "No se pudo obtener el usuario logueado";
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult AceptarRechazarSolicitudes(int id, string accion)
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    Solicitud solicitud = _sistema.BuscarSolicitud(id);
                    if (solicitud!=null)
                    {
                        if (accion.Trim().ToLower() == "aceptar")
                        {
                            _sistema.AceptarSolicitud(solicitud);
                            ViewBag.Mensaje = "Solicitud Aceptada";
                        }
                        else
                        {
                            _sistema.RechazarSolicitud(solicitud);
                            ViewBag.Mensaje = "Solicitud Rechazada";
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "No existe la solicitud";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo obtener el usuario logueado";
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }

        public IActionResult RealizarPublicacion()
        {

            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    if (miembroLogueado.Bloqueado)
                    {
                        ViewBag.Mensaje = "No puedes realizar post, estas bloqueado";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo obtener el usuario logueado";
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }

        [HttpPost]
        public IActionResult RealizarPublicacion(string titulo, string contenido, string privacidad, string imagen)
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    bool publico = false;
                    if (privacidad.Trim().ToLower()=="publico")
                    {
                        publico = true;
                    }
                    try
                    {
                        _sistema.NuevoPost(contenido, miembroLogueado, titulo, publico, imagen);
                        ViewBag.Mensaje = "Se ha publicado con éxito";
                    }
                    catch(Exception e)
                    {
                        ViewBag.Mensaje = e.Message;
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo obtener el usuario logueado";
                }
            }
            else { 
                return RedirectToAction("Login", "Usuario");
            }
            
            return View();
        }


        public IActionResult ListarPostElegidos()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {
                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Usuario");
        }

        [HttpPost]
        public IActionResult ListarPostElegidos(string texto, int aceptacion)
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("tipo") == "MIEMBRO")
            {

                Miembro miembroLogueado = (Miembro)_sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
                if (miembroLogueado != null)
                {
                    List<Publicacion> publicaciones = new List<Publicacion>();
                    try
                    {
                        publicaciones = _sistema.DevolverPublicacionesConCondicion(texto, aceptacion, miembroLogueado);
                        if (publicaciones != null && publicaciones.Count > 0)
                        {
                            ViewBag.Publicaciones = publicaciones;
                        }
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Mensaje = ex.Message;
                    }
                    return View();
                }
            }
            return RedirectToAction("Login", "Usuario");
        }
    }
}
