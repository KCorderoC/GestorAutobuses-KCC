using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestiondeBuses.Plantilla;
using GestordeBuses.Negocio;
using GestordeBusesDatos;

namespace GestordeBuses.Vista
{
    public partial class pantallaPrincipal : Form
    {
        private Usuario _usuario;
        private ChoferNegocio choferNegocio;

        public pantallaPrincipal(Usuario user)
        {
            InitializeComponent();
            _usuario = user;
            choferNegocio = new ChoferNegocio();
            CargarChoferes();
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Chofer chofer = new Chofer
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellidos.Text,
                    Cedula = txtCedula.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text,
                    TipoLicencia = comboLicencia.Text,
                    Estado = comboEstado.Text
                };

                bool insertado = choferNegocio.CrearChofer(chofer);

                if (insertado)
                {
                    MessageBox.Show("Chofer agregado correctamente");
                    CargarChoferes();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el chofer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool TieneAcceso()
        {
            if (_usuario == null)
            {
                MessageBox.Show("Error: Usuario no inicializado.");
                return false;
            }

            if (!_usuario.EsAdmin)
            {
                MessageBox.Show("🚫 No tienes permisos para acceder a este apartado.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un chofer para editar");
                    return;
                }

                int idChofer = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdChofer"].Value);

                Chofer chofer = new Chofer
                {
                    IdChofer = idChofer,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellidos.Text,
                    Cedula = txtCedula.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text,
                    TipoLicencia = comboLicencia.Text,
                    Estado = comboEstado.Text
                };

                bool actualizado = choferNegocio.ActualizarChofer(chofer);

                if (actualizado)
                {
                    MessageBox.Show("Chofer actualizado correctamente");
                    CargarChoferes();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el chofer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un chofer para eliminar");
                    return;
                }

                int idChofer = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdChofer"].Value);

                bool eliminado = choferNegocio.EliminarChofer(idChofer);

                if (eliminado)
                {
                    MessageBox.Show("Chofer eliminado correctamente");
                    CargarChoferes();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el chofer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CargarChoferes()
        {
            try
            {
                DataTable dt = choferNegocio.ListarChoferes();
                dgvTabla.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar choferes: " + ex.Message);
            }
        }

        private void dgvChoferes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvTabla_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void close1_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Login"]?.Show();
        }

        // ========================================
        // BOTONES DE NAVEGACIÓN - CORREGIDOS ✅
        // ========================================

        private void btnAutobuses_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (GBus frm = new GBus(_usuario)) // ✅ PASA EL USUARIO
            {
                frm.ShowDialog();
            }
            this.Show();
        }

        private void btnRutas_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (GRutas frm = new GRutas(_usuario)) // ✅ PASA EL USUARIO
            {
                frm.ShowDialog();
            }
            this.Show();
        }

        private void btnAsignaciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (GAsignaciones frm = new GAsignaciones(_usuario)) // ✅ PASA EL USUARIO
            {
                frm.ShowDialog();
            }
            this.Show();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (GUsers frm = new GUsers(_usuario)) // ✅ PASA EL USUARIO
            {
                frm.ShowDialog();
            }
            this.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e) { }
    }
}