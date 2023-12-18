using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            Usuario adminLogueado = _sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
            string tipo = HttpContext.Session.GetString("tipo");
            
            if (adminLogueado != null && tipo.Trim().ToUpper() == "ADMIN")
            {
                return View();
            } 
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        public IActionResult ListarUsuarios()
        {
            Usuario adminLogueado = _sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
            string tipo = HttpContext.Session.GetString("tipo");
            if (adminLogueado != null && tipo.Trim().ToUpper() == "ADMIN")
            {
                List<Miembro> lista = _sistema.ObtenerListaDeMiembrosOrdenadosPorApellido();
                if (lista.Count > 0)
                {
                    ViewBag.ListaDeMiembros = lista;
                }
                else
                {
                    ViewBag.ListaDeMiembros = "No hay miembros que mostrar";
                }
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        public IActionResult BloquearMiembros()
        {
            Usuario adminLogueado = _sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
            string tipo = HttpContext.Session.GetString("tipo");
            if (adminLogueado != null && tipo.Trim().ToUpper() == "ADMIN")
            {
                List<Miembro> lista = _sistema.ObtenerMiembrosNoBloqueados();
                if (lista.Count > 0)
                {
                    ViewBag.ListaDeMiembrosNoBloqueados = lista;
                }
                else
                {
                    ViewBag.ListaDeMiembrosNoBloqueados = "No hay miembros disponibles para bloquear";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }

        }

        [HttpPost]
        public IActionResult BloquearMiembros(string email) {
            Usuario adminLogueado = _sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
            string tipo = HttpContext.Session.GetString("tipo");

            if(adminLogueado != null && tipo.Trim().ToUpper() == "ADMIN"){ 
                Miembro usuarioBuscado = null;
                usuarioBuscado = (Miembro)_sistema.BuscarUsuario(email);

                if (usuarioBuscado!=null)
                {
                    try
                    {
                        _sistema.BloquearUsuario(usuarioBuscado);
                        ViewBag.Msj = "Usuario bloqueado correctamente";
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Msj = ex.Message;
                    }
                }
                else
                {
                    ViewBag.Msj = "No se encontró al usuario en el sistema";
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }


        }

        public IActionResult BanearPost()
        {
            Usuario adminLogueado = _sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
            string tipo = HttpContext.Session.GetString("tipo");

            if (adminLogueado != null && tipo.Trim().ToUpper() == "ADMIN")
            {
                List<Post> lista = _sistema.ObtenerListaDePosts();
                ViewBag.ListaDePost = lista;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        [HttpPost]
        public IActionResult BanearPost(int idPost)
        {

            Usuario adminLogueado = _sistema.BuscarUsuario(email: HttpContext.Session.GetString("email"));
            string tipo = HttpContext.Session.GetString("tipo");

            if (adminLogueado != null && tipo.Trim().ToUpper() == "ADMIN")
            {
                Post post = (Post)_sistema.BuscarPost(idPost);
                if (post != null)
                {
                    if (!post.Censurado)
                    {
                        try
                        {
                            _sistema.BanearPost(post);
                            ViewBag.Msj = "Post baneado con éxito";
                            return View();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Msj = "Eror al banear el post";
                        }
                    }
                    else
                    {
                        ViewBag.Msj = "El post ya está censurado";
                    }
                }
                else
                {
                    ViewBag.Msj = "No se pudo obtener el post";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

    }
}
