using GestordeBuses.Negocio;
using GestordeBusesDatos;
using GestiondeBuses.Plantilla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestordeBuses.Vista
{
    public partial class Login : Form
    {
        private UsuarioNegocio usuarioNegocio;

        public Login()
        {
            InitializeComponent();
            usuarioNegocio = new UsuarioNegocio();  
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }




        /*
                private void btnIniciar_Click(object sender, EventArgs e)
                {
                    string usuario = txtUser.Text.Trim();
                    string contrasena = txtContraseña.Text.Trim();

                    if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
                    {
                        MessageBox.Show("⚠️ Debes ingresar usuario y contraseña");
                        return;
                    }

                    try
                    {
                        Usuario user = usuarioNegocio.ValidarCredenciales(usuario, contrasena);
                        //Usuario no tiene referencia
                        if (user != null)
                        {
                            if (user.Estado != "Activo")
                            {
                                MessageBox.Show("⚠️ El usuario está inactivo.");
                                return;
                            }

                            MessageBox.Show("✅ Bienvenido " + user.NombreCompleto + " (" + user.Rol + ")");
                            pantallaPrincipal frm = new pantallaPrincipal(user);
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("❌ Usuario o contraseña incorrectos");
                            txtUser.Clear();
                            txtContraseña.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error en la conexión: " + ex.Message);
                    }

                }
              */
        //Este lo pasa a pantallaPrincipal y gasignacion
         
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string usuario = txtUser.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("⚠️ Debes ingresar usuario y contraseña");
                return;
            }

            try
            {
                Usuario user = usuarioNegocio.ValidarCredenciales(usuario, contrasena);

                if (user != null)
                {
                    if (user.Estado != "Activo")
                    {
                        MessageBox.Show("⚠️ El usuario está inactivo.");
                        return;
                    }

                    MessageBox.Show("✅ Bienvenido " + user.NombreCompleto + " (" + user.Rol + ")");

                    // Si es Admin → Pantalla principal
                    if (user.EsAdmin)
                    {
                        pantallaPrincipal frm = new pantallaPrincipal(user);
                        frm.Show();
                    }
                    else
                    {
                        // Si es usuario normal → GAsignacion
                        GAsignaciones frm = new GAsignaciones(user);
                        frm.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("❌ Usuario o contraseña incorrectos");
                    txtUser.Clear();
                    txtContraseña.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexión: " + ex.Message);
            }
        }
        

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


    }
}


