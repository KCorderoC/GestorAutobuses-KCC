using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeBuses.Plantilla
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
        public bool EsAdmin => Rol.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        public bool EstaActivo => Estado == "Activo";



           
        

    }
}
