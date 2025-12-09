using GestiondeBuses.Plantilla;
using GestordeBusesDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestordeBuses.Negocio
{
    public class AutobusNegocio
    {
        private AutobusDatos autobusDatos;

        public AutobusNegocio()
        {
            autobusDatos = new AutobusDatos();
        }

        public List<Autobus> ObtenerAutobuses()
        {
            try
            {
                return autobusDatos.ListarAutobuses();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener autobuses: " + ex.Message);
            }
        }

        public DataTable ObtenerAutobusesDisponibles()
        {
            try
            {
                return autobusDatos.ListarAutobusesDisponibles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener autobuses disponibles: " + ex.Message);
            }
        }

        public bool RegistrarAutobus(Autobus bus)
        {
            try
            {
                ValidarDatosAutobus(bus);
                return autobusDatos.InsertarAutobus(bus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar autobús: " + ex.Message);
            }
        }

        private void ValidarDatosAutobus(Autobus bus)
        {
            if (string.IsNullOrWhiteSpace(bus.NumeroPlaca))
                throw new ArgumentException("La placa no puede estar vacía");

            if (string.IsNullOrWhiteSpace(bus.Marca))
                throw new ArgumentException("La marca es obligatoria");

            if (string.IsNullOrWhiteSpace(bus.Modelo))
                throw new ArgumentException("El modelo es obligatorio");

            if (bus.Anio < 1980 || bus.Anio > DateTime.Now.Year)
                throw new ArgumentException($"El año debe estar entre 1980 y {DateTime.Now.Year}");

            if (bus.Capacidad <= 0)
                throw new ArgumentException("La capacidad debe ser mayor a 0");

            if (!ValidarPlaca(bus.NumeroPlaca))
                throw new ArgumentException("Formato de placa inválido");
        }

        private bool ValidarPlaca(string placa)
        {
            // Validación básica para placas dominicanas
            return placa.Length >= 6 && placa.Length <= 10;
        }

        public bool EditarAutobus(Autobus bus)
        {
            try
            {
                if (bus.IdAutobus <= 0)
                    throw new ArgumentException("ID de autobús inválido");

                ValidarDatosAutobus(bus);
                return autobusDatos.EditarAutobus(bus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar autobús: " + ex.Message);
            }
        }

        public bool EliminarAutobus(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID de autobús inválido");

                return autobusDatos.EliminarAutobus(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar autobús: " + ex.Message);
            }
        }
    }
}