using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GestordeBusesDatos;
using GestiondeBuses.Plantilla;

namespace GestordeBuses.Negocio
{
    public class UsuarioNegocio
    {
        private UsuarioDatos usuarioDatos;

        public UsuarioNegocio()
        {
            usuarioDatos = new UsuarioDatos();
        }

        public Usuario ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
                {
                    throw new ArgumentException("Usuario y contraseña son obligatorios");
                }

                DataTable dt = usuarioDatos.ValidarUsuario(nombreUsuario, contrasena);

                if (dt.Rows.Count == 0)
                {
                    return null; // Credenciales inválidas
                }

                DataRow row = dt.Rows[0];

                // Verificar que el usuario esté activo
                if (row["Estado"].ToString() != "Activo")
                {
                    throw new InvalidOperationException("Usuario inactivo");
                }

                return new Usuario
                {
                    IdUsuario = Convert.ToInt32(row["IdUsuario"]),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    NombreUsuario = nombreUsuario, // No viene en la consulta
                    Rol = row["Rol"].ToString(),
                    Estado = row["Estado"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la validación de credenciales: " + ex.Message);
            }
        }

        public bool CrearUsuario(Usuario usuario)
        {
            try
            {
                ValidarDatosUsuario(usuario);
                return usuarioDatos.InsertarUsuario(
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.NombreUsuario,
                    usuario.Contrasena,
                    usuario.Rol,
                    usuario.Estado
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear usuario: " + ex.Message);
            }
        }

        private void ValidarDatosUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new ArgumentException("El apellido es obligatorio");

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio");

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                throw new ArgumentException("La contraseña es obligatoria");

            if (usuario.Rol != "Admin" && usuario.Rol != "Usuario")
                throw new ArgumentException("El rol debe ser Admin o Usuario");
        }

        public DataTable ListarUsuarios()
        {
            try
            {
                return usuarioDatos.ListarUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
        }
    }
}