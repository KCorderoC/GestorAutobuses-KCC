using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GestiondeBuses.Plantilla;
using GestordeBusesDatos;

namespace GestordeBuses.Negocio
{
    public class RutaNegocio
    {
        private RutaDatos rutaDatos;

        public RutaNegocio()
        {
            rutaDatos = new RutaDatos();
        }

        public bool CrearRuta(string nombreRuta, string origen, string destino, string duracionEstimada, string estado)
        {
            try
            {
                ValidarDatosRuta(nombreRuta, origen, destino);
                return rutaDatos.InsertarRuta(nombreRuta, origen, destino, duracionEstimada, estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear ruta: " + ex.Message);
            }
        }

        private void ValidarDatosRuta(string nombreRuta, string origen, string destino)
        {
            if (string.IsNullOrWhiteSpace(nombreRuta))
                throw new ArgumentException("El nombre de la ruta es obligatorio");

            if (string.IsNullOrWhiteSpace(origen))
                throw new ArgumentException("El origen es obligatorio");

            if (string.IsNullOrWhiteSpace(destino))
                throw new ArgumentException("El destino es obligatorio");

            if (origen.Equals(destino, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("El origen y destino no pueden ser iguales");
        }

        public DataTable ListarRutas()
        {
            try
            {
                return rutaDatos.ListarRutas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar rutas: " + ex.Message);
            }
        }

        public DataTable ListarRutasDisponibles()
        {
            try
            {
                return rutaDatos.ListarRutasDisponibles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar rutas disponibles: " + ex.Message);
            }
        }

        public bool ActualizarRuta(int idRuta, string nombreRuta, string origen, string destino, string duracionEstimada, string estado)
        {
            try
            {
                if (idRuta <= 0)
                    throw new ArgumentException("ID de ruta inválido");

                ValidarDatosRuta(nombreRuta, origen, destino);
                return rutaDatos.EditarRuta(idRuta, nombreRuta, origen, destino, duracionEstimada, estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar ruta: " + ex.Message);
            }
        }
       


        public bool EliminarRuta(int idRuta)
        {
            try
            {
                if (idRuta <= 0)
                    throw new ArgumentException("ID de ruta inválido");

                return rutaDatos.EliminarRuta(idRuta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar ruta: " + ex.Message);
            }
        }
    }
}