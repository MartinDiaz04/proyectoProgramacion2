using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            //Si ya está logueado no hay que redireccionarlo al login, sino a su controlador correspondiente
            if (HttpContext.Session.GetString("email") != null)
            {
                string tipo = HttpContext.Session.GetString("tipo");
                if (tipo.Trim().ToUpper() == "MIEMBRO")
                {
                    return RedirectToAction("Index", "Miembro");
                }
                else if (tipo.Trim().ToUpper() == "ADMIN")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario nuevoUsuario)
        {
            
            try{
                nuevoUsuario.ValidarDatos();
                Usuario miembroBuscado = _sistema.BuscarUsuario(nuevoUsuario.Email);
                if (miembroBuscado != null)
                {
                    if (miembroBuscado.Clave.Trim().ToLower()==nuevoUsuario.Clave.Trim().ToLower())
                    {
                        //se obtiene tipo de usuario para redireccionar a vista correspondiente
                        string tipo = miembroBuscado.TipoDeUsuario();
                        HttpContext.Session.SetString("tipo", tipo);
                        if (tipo.Trim().ToUpper() == "MIEMBRO")
                        {
                            HttpContext.Session.SetString("email", miembroBuscado.Email);
                            //redirección a IActionResult Index de miembro
                            return RedirectToAction("Index", "Miembro");
                        }
                        else if (tipo.Trim().ToUpper() == "ADMIN")
                        {
                            HttpContext.Session.SetString("email", miembroBuscado.Email);
                            return RedirectToAction("Index", "Admin");
                        }
                        //Esto nunca debería pasar, pero se comprueba por robustez, en caso de que ocurra se redirecciona a login nuevamente
                        else
                        {
                            return RedirectToAction("Login", "Usuario");
                        }
                    }
                    else
                    {
                        ViewBag.Msj = "Contraseña incorrecta";
                    }
                    
                }
                else
                {
                    ViewBag.Msj = "Datos incorrectos";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msj=e.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Registro()
        { 
            return View(); 
        }

        [HttpPost]
        public IActionResult Registro(string email, string clave, string nombre, string apellido, DateTime fechaNacimiento)
        {
            Miembro miembro = new Miembro(email, clave, nombre, apellido, fechaNacimiento);
            try
            {
                _sistema.AltaMiembro(miembro);
                ViewBag.Mensaje = "¡Usuario registrado correctamente!";
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje=ex.Message;
            }
            return View();
        }
    }
}
