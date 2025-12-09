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
    public class AutobusDatos
    {
        public List<Autobus> ListarAutobuses()
        {
            List<Autobus> lista = new List<Autobus>();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarAutobuses", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lista.Add(new Autobus
                            {
                                IdAutobus = Convert.ToInt32(dr["IdAutobus"]),
                                NumeroPlaca = dr["NumeroPlaca"].ToString(),
                                Color = dr["Color"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Anio = Convert.ToInt32(dr["Anio"]),
                                Capacidad = Convert.ToInt32(dr["Capacidad"]),
                                Estado = dr["Estado"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar autobuses: " + ex.Message);
            }
            return lista;
        }

        public DataTable ListarAutobusesDisponibles()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    string query = @"SELECT IdAutobus, Marca + ' ' + Modelo + ' (' + NumeroPlaca + ')' AS DescripcionCompleta 
                                   FROM Autobuses 
                                   WHERE Estado = 'Disponible' 
                                   AND IdAutobus NOT IN (
                                       SELECT IdAutobus FROM Asignaciones WHERE Estado = 'Activa'
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
                throw new Exception("Error al listar autobuses disponibles: " + ex.Message);
            }
            return dt;
        }

        public bool InsertarAutobus(Autobus bus)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarAutobus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Marca", bus.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", bus.Modelo);
                        cmd.Parameters.AddWithValue("@NumeroPlaca", bus.NumeroPlaca);
                        cmd.Parameters.AddWithValue("@Color", bus.Color);
                        cmd.Parameters.AddWithValue("@Anio", bus.Anio);
                        cmd.Parameters.AddWithValue("@Capacidad", bus.Capacidad);
                        cmd.Parameters.AddWithValue("@Estado", bus.Estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar autobús: " + ex.Message);
            }
        }

        public bool EditarAutobus(Autobus bus)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EditarAutobus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAutobus", bus.IdAutobus);
                        cmd.Parameters.AddWithValue("@Marca", bus.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", bus.Modelo);
                        cmd.Parameters.AddWithValue("@NumeroPlaca", bus.NumeroPlaca);
                        cmd.Parameters.AddWithValue("@Color", bus.Color);
                        cmd.Parameters.AddWithValue("@Anio", bus.Anio);
                        cmd.Parameters.AddWithValue("@Capacidad", bus.Capacidad);
                        cmd.Parameters.AddWithValue("@Estado", bus.Estado);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar autobús: " + ex.Message);
            }
        }

        public bool EliminarAutobus(int idAutobus)
        {
            try
            {
                using (SqlConnection conn = Conexion.Instancia.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarAutobus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAutobus", idAutobus);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar autobús: " + ex.Message);
            }
        }
    }
}
