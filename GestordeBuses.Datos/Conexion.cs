using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestordeBusesDatos
{
    //Patron singleton
    public sealed class Conexion
    {
        private static Conexion _instancia = null;
        private static readonly object _lock = new object();
        private string _connectionString;

        private Conexion()
        {
            _connectionString = "Data Source=.;Initial Catalog=GestordeBuses;Integrated Security=True";
        }

        //con esto optenemos la unica instancia
        public static Conexion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                            _instancia = new Conexion();
                    }
                }
                return _instancia;
            }
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
