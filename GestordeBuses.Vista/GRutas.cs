using GestiondeBuses.Plantilla;
using GestordeBuses.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestordeBuses.Vista
{
    public partial class GRutas : Form
    {
        private readonly Usuario _usuario;
        private readonly RutaNegocio _negocio;

        public GRutas()
        {
            InitializeComponent();
            _negocio = new RutaNegocio();
            ConfigurarControles();
            CargarRutas();
        }

        public GRutas(Usuario usuario) : this()
        {
            _usuario = usuario;
        }

        private void ConfigurarControles()
        {
            if (comboEstado != null)
            {
                comboEstado.Items.Clear();
                comboEstado.Items.AddRange(new[] { "Activa", "Inactiva" });
                comboEstado.SelectedIndex = 0;
                comboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            if (dgvTabla != null)
            {
                dgvTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTabla.MultiSelect = false;
                dgvTabla.SelectionChanged += DataGridView1_SelectionChanged;
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTabla.CurrentRow == null) return;

            try
            {
                var row = dgvTabla.CurrentRow;
                if (txtRuta != null) txtRuta.Text = row.Cells["NombreRuta"].Value?.ToString();
                if (txtOrigen != null) txtOrigen.Text = row.Cells["Origen"].Value?.ToString();
                if (txtDestino != null) txtDestino.Text = row.Cells["Destino"].Value?.ToString();
                if (txtDuracion != null) txtDuracion.Text = row.Cells["DuracionEstimada"].Value?.ToString();
                if (comboEstado != null) comboEstado.SelectedItem = row.Cells["Estado"].Value?.ToString();
            }
            catch { }
        }

        private void CargarRutas()
        {
            try
            {
                DataTable dt = _negocio.ListarRutas();
                if (dgvTabla != null)
                    dgvTabla.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar rutas: " + ex.Message);
            }
        }

        private bool ValidarFormulario()
        {
            if (txtRuta == null || string.IsNullOrWhiteSpace(txtRuta.Text))
            {
                MessageBox.Show("El nombre de la ruta es obligatorio");
                return false;
            }
            if (txtOrigen == null || string.IsNullOrWhiteSpace(txtOrigen.Text))
            {
                MessageBox.Show("El origen es obligatorio");
                return false;
            }
            if (txtDestino == null || string.IsNullOrWhiteSpace(txtDestino.Text))
            {
                MessageBox.Show("El destino es obligatorio");
                return false;
            }
            return true;
        }

        private void LimpiarFormulario()
        {
            if (txtRuta != null) txtRuta.Clear();
            if (txtOrigen != null) txtOrigen.Clear();
            if (txtDestino != null) txtDestino.Clear();
            if (txtDuracion != null) txtDuracion.Clear();
            if (comboEstado != null) comboEstado.SelectedIndex = 0;
        }

        // ========================================
        // EVENTOS CRUD
        // ========================================

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                bool ok = _negocio.CrearRuta(
                    txtRuta.Text.Trim(),
                    txtOrigen.Text.Trim(),
                    txtDestino.Text.Trim(),
                    txtDuracion?.Text.Trim() ?? "60 minutos",
                    comboEstado?.SelectedItem?.ToString() ?? "Activa"
                );

                if (ok)
                {
                    MessageBox.Show("Ruta creada correctamente");
                    CargarRutas();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la ruta");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona una ruta para editar");
                    return;
                }

                if (!ValidarFormulario()) return;

                int idRuta = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdRuta"].Value);

                bool ok = _negocio.ActualizarRuta(
                    idRuta,
                    txtRuta.Text.Trim(),
                    txtOrigen.Text.Trim(),
                    txtDestino.Text.Trim(),
                    txtDuracion?.Text.Trim() ?? "60 minutos",
                    comboEstado?.SelectedItem?.ToString() ?? "Activa"
                );

                if (ok)
                {
                    MessageBox.Show("Ruta actualizada correctamente");
                    CargarRutas();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la ruta");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message);
            }
        }

        private void btbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona una ruta para eliminar");
                    return;
                }

                int idRuta = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdRuta"].Value);

                DialogResult result = MessageBox.Show(
                    "¿Seguro que deseas eliminar esta ruta?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    bool ok = _negocio.EliminarRuta(idRuta);

                    if (ok)
                    {
                        MessageBox.Show("Ruta eliminada correctamente");
                        CargarRutas();
                        LimpiarFormulario();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la ruta");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        // ========================================
        // EVENTOS DE NAVEGACIÓN - CORREGIDOS ✅
        // ========================================

        private void btnChoferes_Click(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.EsAdmin)
            {
                pantallaPrincipal frm = new pantallaPrincipal(_usuario);
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("🚫 No tienes permisos para acceder a este apartado.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnAutobuses_Click(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.EsAdmin)
            {
                GBus frm = new GBus(_usuario);
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("🚫 No tienes permisos para acceder a este apartado.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnAsignaciones_Click(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.EsAdmin)
            {
                GAsignaciones frm = new GAsignaciones(_usuario);
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("🚫 No tienes permisos para acceder a este apartado.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.EsAdmin)
            {
                GUsers frm = new GUsers(_usuario);
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("🚫 No tienes permisos para acceder a este apartado.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void close1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Eventos existentes
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox6_Click(object sender, EventArgs e) { }
    }
}
