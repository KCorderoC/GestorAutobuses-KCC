using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GestiondeBuses.Plantilla;

namespace GestordeBusesDatos
{
    public class RutaDatos
    {
        public bool InsertarRuta(string nombreRuta, string origen, string destino, string duracionEstimada, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarRuta", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreRuta", nombreRuta);
                        cmd.Parameters.AddWithValue("@Origen", origen);
                        cmd.Parameters.AddWithValue("@Destino", destino);
                        cmd.Parameters.AddWithValue("@DuracionEstimada", duracionEstimada);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar ruta: " + ex.Message);
            }
        }

        public DataTable ListarRutas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarRutas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar rutas: " + ex.Message);
            }
            return dt;
        }

        public DataTable ListarRutasDisponibles()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    string query = @"SELECT IdRuta, NombreRuta + ' (' + Origen + ' - ' + Destino + ')' AS DescripcionCompleta 
                                   FROM Rutas 
                                   WHERE Estado = 'Activa' 
                                   AND IdRuta NOT IN (
                                       SELECT IdRuta FROM Asignaciones WHERE Estado = 'Activa'
                                   )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar rutas disponibles: " + ex.Message);
            }
            return dt;
        }

        public bool EditarRuta(int idRuta, string nombreRuta, string origen, string destino, string duracionEstimada, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EditarRuta", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRuta", idRuta);
                        cmd.Parameters.AddWithValue("@NombreRuta", nombreRuta);
                        cmd.Parameters.AddWithValue("@Origen", origen);
                        cmd.Parameters.AddWithValue("@Destino", destino);
                        cmd.Parameters.AddWithValue("@DuracionEstimada", duracionEstimada);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar ruta: " + ex.Message);
            }
        }

        public bool EliminarRuta(int idRuta)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarRuta", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRuta", idRuta);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar ruta: " + ex.Message);
            }
        }
    }
}