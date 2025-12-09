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
    public class AsignacionNegocio
    {
        private AsignacionDatos asignacionDatos;
        private ChoferDatos choferDatos;
        private AutobusDatos autobusDatos;
        private RutaDatos rutaDatos;

        public AsignacionNegocio()
        {
            asignacionDatos = new AsignacionDatos();
            choferDatos = new ChoferDatos();
            autobusDatos = new AutobusDatos();
            rutaDatos = new RutaDatos();
        }

        public bool CrearAsignacion(int idChofer, int idAutobus, int idRuta, DateTime fechaAsignacion, TimeSpan horaSalida)
        {
            try
            {
                ValidarDisponibilidad(idChofer, idAutobus, idRuta);
                ValidarDatosAsignacion(fechaAsignacion, horaSalida);

                return asignacionDatos.InsertarAsignacion(idChofer, idAutobus, idRuta, fechaAsignacion, horaSalida, "Activa");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear asignación: " + ex.Message);
            }
        }

        private void ValidarDisponibilidad(int idChofer, int idAutobus, int idRuta)
        {
            // Validar que el chofer esté disponible
            var choferesDisponibles = choferDatos.ListarChoferesDisponibles();
            bool choferDisponible = false;
            foreach (DataRow row in choferesDisponibles.Rows)
            {
                if (Convert.ToInt32(row["IdChofer"]) == idChofer)
                {
                    choferDisponible = true;
                    break;
                }
            }
            if (!choferDisponible)
                throw new InvalidOperationException("El chofer seleccionado no está disponible");

            // Validar que el autobús esté disponible
            var autobusesDisponibles = autobusDatos.ListarAutobusesDisponibles();
            bool autobusDisponible = false;
            foreach (DataRow row in autobusesDisponibles.Rows)
            {
                if (Convert.ToInt32(row["IdAutobus"]) == idAutobus)
                {
                    autobusDisponible = true;
                    break;
                }
            }
            if (!autobusDisponible)
                throw new InvalidOperationException("El autobús seleccionado no está disponible");

            // Validar que la ruta esté disponible
            var rutasDisponibles = rutaDatos.ListarRutasDisponibles();
            bool rutaDisponible = false;
            foreach (DataRow row in rutasDisponibles.Rows)
            {
                if (Convert.ToInt32(row["IdRuta"]) == idRuta)
                {
                    rutaDisponible = true;
                    break;
                }
            }
            if (!rutaDisponible)
                throw new InvalidOperationException("La ruta seleccionada no está disponible");
        }

        private void ValidarDatosAsignacion(DateTime fechaAsignacion, TimeSpan horaSalida)
        {
            if (fechaAsignacion.Date < DateTime.Today)
                throw new ArgumentException("La fecha de asignación no puede ser anterior a hoy");

            if (horaSalida < TimeSpan.Zero || horaSalida >= TimeSpan.FromHours(24))
                throw new ArgumentException("La hora de salida no es válida");
        }

        public DataTable ListarAsignaciones()
        {
            try
            {
                return asignacionDatos.ListarAsignaciones();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar asignaciones: " + ex.Message);
            }
        }

        public bool FinalizarAsignacion(int idAsignacion)
        {
            try
            {
                if (idAsignacion <= 0)
                    throw new ArgumentException("ID de asignación inválido");

                // Obtener datos actuales de la asignación
                var asignaciones = ListarAsignaciones();
                DataRow asignacionActual = null;

                foreach (DataRow row in asignaciones.Rows)
                {
                    if (Convert.ToInt32(row["IdAsignacion"]) == idAsignacion)
                    {
                        asignacionActual = row;
                        break;
                    }
                }

                if (asignacionActual == null)
                    throw new InvalidOperationException("Asignación no encontrada");

                // Cambiar estado a "Finalizada" manteniendo los demás datos
                return asignacionDatos.EditarAsignacion(
                    idAsignacion,
                    Convert.ToInt32(asignacionActual["IdChofer"]),
                    Convert.ToInt32(asignacionActual["IdAutobus"]),
                    Convert.ToInt32(asignacionActual["IdRuta"]),
                    Convert.ToDateTime(asignacionActual["FechaAsignacion"]),
                    TimeSpan.Parse(asignacionActual["HoraSalida"].ToString()),
                    "Finalizada"
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al finalizar asignación: " + ex.Message);
            }
        }

        // Métodos para obtener elementos disponibles (para llenar combos)
        public DataTable ObtenerChoferesDisponibles()
        {
            return choferDatos.ListarChoferesDisponibles();
        }

        public DataTable ObtenerAutobusesDisponibles()
        {
            return autobusDatos.ListarAutobusesDisponibles();
        }

        public DataTable ObtenerRutasDisponibles()
        {
            return rutaDatos.ListarRutasDisponibles();
        }


    }
}