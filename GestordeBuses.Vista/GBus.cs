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

namespace GestordeBuses.Vista
{
    public partial class GBus : Form
    {
        private readonly Usuario _usuario;
        private readonly AutobusNegocio _negocio;

        public GBus()
        {
            InitializeComponent();
            _negocio = new AutobusNegocio();
            ConfigurarControles();
            CargarEstados();
            CargarAutobuses();
        }

        public GBus(Usuario usuario) : this()
        {
            _usuario = usuario;
        }

        private void ConfigurarControles()
        {
            comboEstados.DropDownStyle = ComboBoxStyle.DropDownList;
            dgvTabla.AutoGenerateColumns = true;
            dgvTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTabla.MultiSelect = false;

            nudAnio.Minimum = 1980;
            nudAnio.Maximum = DateTime.Now.Year;
            nudAnio.Value = DateTime.Now.Year;

            nudCapacidad.Minimum = 1;
            nudCapacidad.Maximum = 100;
            nudCapacidad.Value = 20;

            dgvTabla.SelectionChanged += DgvTabla_SelectionChanged;
        }

        private void DgvTabla_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTabla.CurrentRow == null) return;

            try
            {
                var row = dgvTabla.CurrentRow;
                txtPlaca.Text = row.Cells["NumeroPlaca"].Value?.ToString();
                txtMarca.Text = row.Cells["Marca"].Value?.ToString();
                txtModelo.Text = row.Cells["Modelo"].Value?.ToString();
                txtColor.Text = row.Cells["Color"].Value?.ToString();

                if (int.TryParse(row.Cells["Anio"].Value?.ToString(), out int anio))
                    nudAnio.Value = anio;

                if (int.TryParse(row.Cells["Capacidad"].Value?.ToString(), out int capacidad))
                    nudCapacidad.Value = capacidad;

                comboEstados.SelectedItem = row.Cells["Estado"].Value?.ToString();
            }
            catch
            {
                // Error silencioso al cargar datos de la fila
            }
        }

        private void CargarEstados()
        {
            comboEstados.Items.Clear();
            comboEstados.Items.AddRange(new[] { "Disponible", "En mantenimiento", "Fuera de servicio" });
            comboEstados.SelectedIndex = 0;
        }

        private void CargarAutobuses()
        {
            try
            {
                var autobuses = _negocio.ObtenerAutobuses();

                DataTable dt = new DataTable();
                dt.Columns.Add("IdAutobus", typeof(int));
                dt.Columns.Add("NumeroPlaca", typeof(string));
                dt.Columns.Add("Marca", typeof(string));
                dt.Columns.Add("Modelo", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("Anio", typeof(int));
                dt.Columns.Add("Capacidad", typeof(int));
                dt.Columns.Add("Estado", typeof(string));

                foreach (var bus in autobuses)
                {
                    dt.Rows.Add(bus.IdAutobus, bus.NumeroPlaca, bus.Marca,
                               bus.Modelo, bus.Color, bus.Anio, bus.Capacidad, bus.Estado);
                }

                dgvTabla.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar autobuses: " + ex.Message);
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                MessageBox.Show("La placa es obligatoria");
                txtPlaca.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                MessageBox.Show("La marca es obligatoria");
                txtMarca.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("El modelo es obligatorio");
                txtModelo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtColor.Text))
            {
                MessageBox.Show("El color es obligatorio");
                txtColor.Focus();
                return false;
            }

            if (comboEstados.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estado");
                comboEstados.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtPlaca.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtColor.Clear();
            nudAnio.Value = DateTime.Now.Year;
            nudCapacidad.Value = 20;
            comboEstados.SelectedIndex = 0;
        }

        // ========================================
        // EVENTOS DE NAVEGACIÓN - CORREGIDOS ✅
        // ========================================

        private void close1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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

        private void btnRutas_Click(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.EsAdmin)
            {
                GRutas frm = new GRutas(_usuario);
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

        // ========================================
        // EVENTOS CRUD
        // ========================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                var nuevo = new Autobus
                {
                    NumeroPlaca = txtPlaca.Text.Trim(),
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim(),
                    Color = txtColor.Text.Trim(),
                    Anio = (int)nudAnio.Value,
                    Capacidad = (int)nudCapacidad.Value,
                    Estado = comboEstados.SelectedItem.ToString()
                };

                bool ok = _negocio.RegistrarAutobus(nuevo);
                if (ok)
                {
                    MessageBox.Show("Autobús registrado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAutobuses();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el autobús", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar autobús: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un autobús para editar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarFormulario()) return;

                int idAutobus = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdAutobus"].Value);

                var autobus = new Autobus
                {
                    IdAutobus = idAutobus,
                    NumeroPlaca = txtPlaca.Text.Trim(),
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim(),
                    Color = txtColor.Text.Trim(),
                    Anio = (int)nudAnio.Value,
                    Capacidad = (int)nudCapacidad.Value,
                    Estado = comboEstados.SelectedItem.ToString()
                };

                bool ok = _negocio.EditarAutobus(autobus);
                if (ok)
                {
                    MessageBox.Show("Autobús actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAutobuses();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el autobús", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar autobús: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un autobús para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdAutobus"].Value);
                string placa = dgvTabla.CurrentRow.Cells["NumeroPlaca"].Value.ToString();

                var resultado = MessageBox.Show($"¿Está seguro que desea eliminar el autobús {placa}?",
                                              "Confirmar eliminación",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes) return;

                bool ok = _negocio.EliminarAutobus(id);
                if (ok)
                {
                    MessageBox.Show("Autobús eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAutobuses();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el autobús", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar autobús: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENTOS DE CONTROLES
        private void pictureBox10_Click(object sender, EventArgs e) { }
        private void pictureBox11_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void nudAño_ValueChanged(object sender, EventArgs e) { }
        private void txtMarca_TextChanged(object sender, EventArgs e) { }
        private void pictureBox5_Click(object sender, EventArgs e) { }
    }
}