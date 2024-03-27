using System;
using System.IO;
namespace MycGroupApp
{
    static class Router
    {
        static string user = Environment.UserName;

        static string production = "https://agrosalta-production.up.railway.app";
        static string desarrollo = "http://localhost:3000";

        //static public string Usuarios { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\usrs.txt"; } }
        static public string Cliente { get { return production+"/api/v1/clientes"; } }
        static public string ClienteVehiculo { get { return production + "/api/v1/clientes/vehiculo"; } }
        static public string Movimiento { get { return production + "/api/v1/movimientos"; } }
        static public string Oficina { get { return production + "/api/v1/oficinas"; } }
        static public string Operacion { get { return production + "/api/v1/operaciones"; } }
        static public string Servicio { get { return production + "/api/v1/servicios"; } }
        static public string Precio { get { return production + "/api/v1/precios"; } }
        static public string Usuario { get { return production + "/api/v1/usuarios"; } }
        static public string Vehiculo { get { return production + "/api/v1/vehiculos"; } }
        static public string Login { get { return production + "/api/v1/login"; } }
           
        // static public string OficinaLocal { get { return @"C:\\Users\\" + user + "\\NroOfa.txt"; } }
        static public string OficinaLocal { get { return IndexOficinaLocal().ToString(); } set { IndexOficinaLocal(value); } }
        static public string Archivos{ get { return @"C:\\Users\\" + user + "\\MyCgroupLocal\\PdfM&C\\"; } }
        static public string Captura { get { return @"C:\\Users\\" + user + "\\MyCgroupLocal\\captura_pantalla.png"; } }

        static public int IndexOficinaLocal()
        {
            string rutaCarpeta = @"C:\\Users\\" + user + "\\MyCgroupLocal";
            string rutaArchivo = @"C:\\Users\\" + user + "\\MyCgroupLocal\\NroOfa.txt";
            int i = 0;
            if (Directory.Exists(rutaCarpeta)&& File.Exists(rutaArchivo) )
            {
              

                StreamReader p = new StreamReader(rutaArchivo);

                string l=p.ReadLine();

                try
                {
                    i = int.Parse(l);
                }
                catch (Exception ){ i = 0; }
                p.Close(); p.Dispose();
            }
            else
            {
                try
                {
                    

                    if (Directory.Exists(rutaCarpeta)==false) { Directory.CreateDirectory(rutaCarpeta); }
                    File.Create(rutaArchivo);
                }
                catch (Exception er) { i = -1; }   
            }

            return i;
        }

        static public int IndexOficinaLocal(string l)
        {
            string rutaCarpeta = @"C:\\Users\\" + user + "\\MyCgroupLocal";
            string rutaArchivo = @"C:\\Users\\" + user + "\\MyCgroupLocal\\NroOfa.txt";
            int i = 0;
            if (Directory.Exists(rutaCarpeta) && File.Exists(rutaArchivo))
            {

                StreamWriter p = new StreamWriter(rutaArchivo);

                p.WriteLine(l);
                p.Close(); p.Dispose();
            }
            else
            {
                try
                {
                    if (Directory.Exists(rutaCarpeta) == false) { Directory.CreateDirectory(rutaCarpeta); };
                    File.Create(rutaArchivo);
                    StreamWriter p = new StreamWriter(rutaArchivo);
                    p.WriteLine(l);
                    p.Close(); p.Dispose();
                }
                catch (Exception er) { i = -1; }
            }

            return i;
        }
    
        static public bool EstadoUso { get { return true; } }    
    
    }
}
