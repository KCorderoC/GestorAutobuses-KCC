using GestordeBusesDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestordeBusesDatos
{
    public class ChoferDatos
    {
        public bool InsertarChofer(string nombre, string apellido, string cedula,
                                 string telefono, string direccion, string tipoLicencia, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarChofer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@Cedula", cedula);
                        cmd.Parameters.AddWithValue("@Telefono", telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Direccion", direccion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoLicencia", tipoLicencia);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar chofer: " + ex.Message);
            }
        }
        public DataTable ListarChoferes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarChoferes", conn))
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
                throw new Exception("Error al listar choferes: " + ex.Message);
            }
            return dt;
        }
        public DataTable ListarChoferesDisponibles()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    string query = @"SELECT IdChofer, Nombre + ' ' + Apellido AS NombreCompleto 
                                   FROM Choferes 
                                   WHERE Estado = 'Activo' 
                                   AND IdChofer NOT IN (
                                       SELECT IdChofer FROM Asignaciones WHERE Estado = 'Activa'
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
                throw new Exception("Error al listar choferes disponibles: " + ex.Message);
            }
            return dt;
        }

        public bool EditarChofer(int idChofer, string nombre, string apellido, string cedula,
                               string telefono, string direccion, string tipoLicencia, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EditarChofer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdChofer", idChofer);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@Cedula", cedula);
                        cmd.Parameters.AddWithValue("@Telefono", telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Direccion", direccion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoLicencia", tipoLicencia);
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar chofer: " + ex.Message);
            }
        }

        public bool EliminarChofer(int idChofer)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarChofer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdChofer", idChofer);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar chofer: " + ex.Message);
            }
        }
    }
}








