using LogicaNegocio;
using System.ComponentModel;
using System.Security.Authentication;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InterfazDeUsuario
{
    internal class Program
    {

        public static Sistema _sistema = new Sistema();

        static void Main(string[] args)
        {
            int opcion = -1;
            while (opcion != 6) { 
                MostrarMenu();
                Console.WriteLine("Ingrese la opción deseada");
                string opcionTxt = Console.ReadLine();
                int.TryParse(opcionTxt, out opcion);
                EvaluarOpcion(opcion);
            }
        }




        static void MostrarMenu()
        {

            Console.WriteLine("Ingrese la opción deseada");
            Console.WriteLine("1 - Alta Miembro");
            Console.WriteLine("2 - Ingesar email y verificar publicaciones");
            Console.WriteLine("3 - Ingresar email y ver posts que ha comentado");
            Console.WriteLine("4 - Ingresar rango de fechas y mostrar publicaciones entre esas fechas");
            Console.WriteLine("5 - Mostrar miembros con mas publicaciones y comentarios");
            Console.WriteLine("6 - Salir");
        }

        static void EvaluarOpcion(int opcion) {
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Alta de miembro");
                    AltaMiembro();
                    break;
                case 2:
                    Console.WriteLine("Verificar publicaciones");
                    VerificarPublicaciones();
                    break;
                case 3:
                    Console.WriteLine("Ver comentarios en post");
                    VerPostsComentados();
                    break;
                case 4:
                    Console.WriteLine("Ingresar rango de fechas y mostrar posts");
                    PostsEnRangoDeFechas();
                    break;
                case 5:
                    Console.WriteLine("Mostrar miembros con mas publicaciones");
                    MiembrosConMasPublicaciones();
                    break;
                case 6:
                    break;
            }
        }

        static void AltaMiembro()
        {
            try
            {
                Console.WriteLine("Ingrese el email");
                string email = Console.ReadLine();
                Console.WriteLine("Ingrese la contraseña");
                string clave = Console.ReadLine();
                Console.WriteLine("Ingrese su nombre");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese su apellido");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese su fecha de nacimiento");
                DateTime.TryParse(Console.ReadLine(), out DateTime fechaNacimiento);

                Miembro nuevoMiembro = new Miembro(email, clave, nombre, apellido, fechaNacimiento);
                _sistema.AltaMiembro(nuevoMiembro);
                
                Console.WriteLine("Miembro ingresado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //Crear nueva publicación y agregarla a lista de publicaciones de miembro
        static void nuevoPost(Miembro miembro)
        {
            try
            {
                Post nuevaPublicacion = new Post("Contenido en texto del post", miembro, "titulo del post", true, "perrito.png");
                _sistema.NuevoPost(miembro, nuevaPublicacion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString);
            }
        }

        static void nuevoComentario(Miembro miembro, Post postAsociado)
        {
            try
            {
                Comentario nuevoComentario = new Comentario("Este es el contenido del comentario", miembro, "Titulo del comentario", true);
                _sistema.NuevoComentario(miembro, nuevoComentario, postAsociado);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        static void VerificarPublicaciones()
        {
            List<Publicacion> publicaciones = new List<Publicacion>();
            try
            {
                Console.WriteLine("Ingrese el email que quiere verificar");
                string email = Console.ReadLine();
                
                publicaciones = _sistema.DevolverPublicaciones(email.Trim().ToLower());

                if (email.Trim().Length > 0)
                {
                    if (publicaciones.Count==0)
                    {
                        Console.WriteLine("No hay publicaciones asociadas a ese email");
                    }
                    else
                    {
                        for (int n=0; n<publicaciones.Count; n++)
                        {
                            Console.WriteLine(publicaciones[n]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("El email ingresado está vacío");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString);
            }
            

            
        }


        static void EnviarSolicitud()
        {
            try
            {
                // PEDIR EMAIL DE SOLICITANTE Y SOLICITADO
                Console.WriteLine("Ingrese el email del miembro solicitante");
                string emailMiembroSolicitante = Console.ReadLine();
                Console.WriteLine("Ingrese el email del miembro solicitado");
                string emailMiembroSolicitado = Console.ReadLine();

                // INICIALIZO VARIABLES
                Miembro miembroSolicitante = (Miembro)_sistema.BuscarUsuario(emailMiembroSolicitante);
                Miembro miembroSolicitado = (Miembro)_sistema.BuscarUsuario(emailMiembroSolicitado);


                // Si no son vacios creo el objeto solicitud
                if (miembroSolicitante != null && miembroSolicitado != null)
                {
                    _sistema.EnviarSolicitud(miembroSolicitante, miembroSolicitado);
                }
                else
                {
                    Console.WriteLine("No se encontró usuario asociado a ese email");
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString);
            }
           
        }




        // MOSTRAR LISTA DE PUBLICACIONES Y USUARIOS, MOSTRANDO LA LISTA DE SISTEMA Y USANDO UN TOSTRING EN PUBLICACION Y EN USUARIO

        // VER ESTO
        static void MostrarPublicacionUsuarios()
        {
            for (int n = 0; n < _sistema.Publicaciones.Count; n++)
            {
                Publicacion publicacionActual = _sistema.Publicaciones[n];
                Console.WriteLine(publicacionActual);
            }
            for (int n = 0; n < _sistema.Usuarios.Count; n++)
            {
                Usuario usuarioActual = _sistema.Usuarios[n];
                Console.WriteLine(usuarioActual);
            }
        }

        static void VerPostsComentados()
        {
            List<Comentario> comentarios = new List<Comentario>();  
            try
            {
                Console.WriteLine("Ingrese email");
                string email = Console.ReadLine();
                if (email.Trim().Length > 0)
                {
                    comentarios = _sistema.DevolverComentariosDeUnUsuario(email.Trim().ToLower());
                    if (comentarios.Count>0)
                    {
                        for (int n = 0; n<comentarios.Count; n++)
                        {
                            Console.WriteLine(comentarios[n]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron comentarios de este usuario");
                    }
                }
                else
                {
                    Console.WriteLine("El email ingresado está vacío");
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString);
            }
            
                       
        }

        static void PostsEnRangoDeFechas()
        {
            try
            {
                Console.WriteLine("Ingrese la primera fecha DD-MM-YYYY");
                DateTime.TryParse(Console.ReadLine(), out DateTime primeraFecha);
                Console.WriteLine("Ingrese la segunda fecha DD-MM-YYYY");
                DateTime.TryParse(Console.ReadLine(), out DateTime segundaFecha);
                
                //contempla caso en que usuario ingrese fecha vacía 
                if (primeraFecha.ToString()== "01/01/0001 0:00:00" || segundaFecha.ToString() == "01/01/0001 0:00:00")
                {
                    Console.WriteLine("Las fechas ingresadas no son correctas");
                } 
                else
                {
                    List<Publicacion> listaPublicacionesEntreFechas = _sistema.ObtenerPublicacionesEntreDosFechas(primeraFecha, segundaFecha);
                    List<Publicacion> listaOrdenada = _sistema.OrdenarLista(listaPublicacionesEntreFechas);
                    for (int n = 0; n < listaOrdenada.Count; n++)
                    {
                        Console.WriteLine(_sistema.RetornarPublicacion(listaOrdenada[n]));
                    }
                }
                
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
            
        }

        static void MiembrosConMasPublicaciones()
        {
            try
            {
                Console.WriteLine("Miembro con mas publicaciones");
                List<Miembro> listaMiembro = new List<Miembro>();
                listaMiembro = _sistema.DevolverUsuariosConMasPublicaciones();
                foreach (Miembro miembro in listaMiembro)
                {
                    Console.WriteLine(miembro);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        }   
    }