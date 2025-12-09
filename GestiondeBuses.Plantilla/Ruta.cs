using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeBuses.Plantilla
{
    public class Ruta
    {
        public int IdRuta { get; set; }
        public string NombreRuta { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Estado { get; set; }

        public string DescripcionCompleta => $"{NombreRuta} ({Origen} - {Destino})";
        public bool EstaActiva => Estado == "Activa";
    }
}
