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
    public class UsuarioDatos
    {
        public DataTable ValidarUsuario(string usuario, string contrasena)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LoginUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar usuario: " + ex.Message);
            }
            return dt;
        }

        public bool InsertarUsuario(string nombre, string apellido, string usuario,
                                   string contrasena, string rol, string estado)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@Rol", rol);
                        cmd.Parameters.AddWithValue("@Estado", estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuario: " + ex.Message);
            }
        }

        public DataTable ListarUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarUsuarios", conn))
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
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
            return dt;
        }
    }
}


