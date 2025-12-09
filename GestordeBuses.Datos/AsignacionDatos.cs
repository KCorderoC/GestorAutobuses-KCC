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
    public class AsignacionDatos
    {
        public bool InsertarAsignacion(int idChofer, int idAutobus, int idRuta, DateTime fechaAsignacion, TimeSpan horaSalida, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarAsignacion", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdChofer", idChofer);
                        cmd.Parameters.AddWithValue("@IdAutobus", idAutobus);
                        cmd.Parameters.AddWithValue("@IdRuta", idRuta);
                        cmd.Parameters.AddWithValue("@FechaAsignacion", fechaAsignacion);
                        cmd.Parameters.AddWithValue("@HoraSalida", horaSalida);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar asignación: " + ex.Message);
            }
        }

        public DataTable ListarAsignaciones()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarAsignaciones", conn))
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
                throw new Exception("Error al listar asignaciones: " + ex.Message);
            }
            return dt;
        }

        public bool EditarAsignacion(int idAsignacion, int idChofer, int idAutobus, int idRuta, DateTime fechaAsignacion, TimeSpan horaSalida, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EditarAsignacion", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAsignacion", idAsignacion);
                        cmd.Parameters.AddWithValue("@IdChofer", idChofer);
                        cmd.Parameters.AddWithValue("@IdAutobus", idAutobus);
                        cmd.Parameters.AddWithValue("@IdRuta", idRuta);
                        cmd.Parameters.AddWithValue("@FechaAsignacion", fechaAsignacion);
                        cmd.Parameters.AddWithValue("@HoraSalida", horaSalida);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar asignación: " + ex.Message);
            }
        }

        public bool EliminarAsignacion(int idAsignacion)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarAsignacion", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAsignacion", idAsignacion);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar asignación: " + ex.Message);
            }
        }
    }
}
