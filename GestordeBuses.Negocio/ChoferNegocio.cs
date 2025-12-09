using GestordeBusesDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeBuses.Plantilla;

namespace GestordeBuses.Negocio
{
    public class ChoferNegocio
    {
        private ChoferDatos choferDatos;

        public ChoferNegocio()
        {
            choferDatos = new ChoferDatos();
        }

        public bool CrearChofer(Chofer chofer)
        {
            try
            {
                ValidarDatosChofer(chofer);

                return choferDatos.InsertarChofer(
                    chofer.Nombre,
                    chofer.Apellido,
                    chofer.Cedula,
                    chofer.Telefono,
                    chofer.Direccion,
                    chofer.TipoLicencia,
                    chofer.Estado
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear chofer: " + ex.Message);
            }
        }

        private void ValidarDatosChofer(Chofer chofer)
        {
            if (string.IsNullOrWhiteSpace(chofer.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(chofer.Apellido))
                throw new ArgumentException("El apellido es obligatorio");

            if (string.IsNullOrWhiteSpace(chofer.Cedula))
                throw new ArgumentException("La cédula es obligatoria");
            if (!ValidarFormatoCedula(chofer.Cedula))
                throw new ArgumentException("Formato de cédula inválido");

            if (chofer.TipoLicencia != "C" && chofer.TipoLicencia != "D")
                throw new ArgumentException("El tipo de licencia debe ser C o D");
        }

        private bool ValidarFormatoCedula(string cedula)
        {
            // Validación básica para cédula dominicana: XXX-XXXXXXX-X
            return cedula.Length >= 11 && cedula.Contains("-");
        }

        public DataTable ListarChoferes()
        {
            try
            {
                return choferDatos.ListarChoferes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar choferes: " + ex.Message);
            }
        }

        public DataTable ListarChoferesDisponibles()
        {
            try
            {
                return choferDatos.ListarChoferesDisponibles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar choferes disponibles: " + ex.Message);
            }
        }

        public bool ActualizarChofer(Chofer chofer)
        {
            try
            {
                ValidarDatosChofer(chofer);

                return choferDatos.EditarChofer(
                    chofer.IdChofer,
                    chofer.Nombre,
                    chofer.Apellido,
                    chofer.Cedula,
                    chofer.Telefono,
                    chofer.Direccion,
                    chofer.TipoLicencia,
                    chofer.Estado
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar chofer: " + ex.Message);
            }
        }

        public bool EliminarChofer(int idChofer)
        {
            try
            {
                if (idChofer <= 0)
                    throw new ArgumentException("ID de chofer inválido");
                return choferDatos.EliminarChofer(idChofer);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar chofer: " + ex.Message);
            }
        }
    }
}



