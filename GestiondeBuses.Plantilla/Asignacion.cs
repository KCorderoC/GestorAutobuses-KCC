using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeBuses.Plantilla
{
    public class Asignacion
    {
        public int IdAsignacion { get; set; }
        public int IdChofer { get; set; }
        public int IdAutobus { get; set; }
        public int IdRuta { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public string Estado { get; set; }

        // Propiedades de navegación
        public string NombreChofer { get; set; }
        public string DescripcionAutobus { get; set; }
        public string NombreRuta { get; set; }

        public bool EstaActiva => Estado == "Activa";
    }
}

