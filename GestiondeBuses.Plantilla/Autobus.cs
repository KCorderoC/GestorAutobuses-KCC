using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeBuses.Plantilla
{
    public class Autobus
    {
        public int IdAutobus { get; set; }
        public string NumeroPlaca { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; }

        public string DescripcionCompleta => $"{Marca} {Modelo} ({NumeroPlaca})";
        public bool EstaDisponible => Estado == "Disponible";
    }
}
