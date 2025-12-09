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
    public partial class GAsignaciones : Form
    {
        private readonly Usuario _usuario;
        private readonly AsignacionNegocio _negocio;

        public GAsignaciones()
        {
            InitializeComponent();
            _negocio = new AsignacionNegocio();
            ConfigurarControles();
            CargarAsignaciones();
            CargarCombos();
        }

        public GAsignaciones(Usuario usuario) : this()
        {
            _usuario = usuario;
        }

        private void ConfigurarControles()
        {
            if (timerFecha != null)
            {
                timerFecha.Value = DateTime.Today;
                timerFecha.Format = DateTimePickerFormat.Short;
            }

            if (timerHora != null)
            {
                timerHora.Format = DateTimePickerFormat.Time;
                timerHora.ShowUpDown = true;
                timerHora.Value = DateTime.Now;
            }

            if (dgvTabla != null)
            {
                dgvTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTabla.MultiSelect = false;
            }
        }

        private void CargarCombos()
        {
            try
            {
                if (comboChofer != null)
                {
                    DataTable choferes = _negocio.ObtenerChoferesDisponibles();
                    comboChofer.DataSource = choferes;
                    comboChofer.DisplayMember = "NombreCompleto";
                    comboChofer.ValueMember = "IdChofer";
                }

                if (comboAutobus != null)
                {
                    DataTable autobuses = _negocio.ObtenerAutobusesDisponibles();
                    comboAutobus.DataSource = autobuses;
                    comboAutobus.DisplayMember = "DescripcionCompleta";
                    comboAutobus.ValueMember = "IdAutobus";
                }

                if (comboRuta != null)
                {
                    DataTable rutas = _negocio.ObtenerRutasDisponibles();
                    comboRuta.DataSource = rutas;
                    comboRuta.DisplayMember = "DescripcionCompleta";
                    comboRuta.ValueMember = "IdRuta";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message);
            }
        }

        private void CargarAsignaciones()
        {
            try
            {
                DataTable dt = _negocio.ListarAsignaciones();
                if (dgvTabla != null)
                    dgvTabla.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asignaciones: " + ex.Message);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboChofer?.SelectedValue == null ||
                    comboAutobus?.SelectedValue == null ||
                    comboRuta?.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar chofer, autobús y ruta");
                    return;
                }

                int idChofer = Convert.ToInt32(comboChofer.SelectedValue);
                int idAutobus = Convert.ToInt32(comboAutobus.SelectedValue);
                int idRuta = Convert.ToInt32(comboRuta.SelectedValue);

                DateTime fecha = timerFecha?.Value.Date ?? DateTime.Today;
                TimeSpan hora = timerHora?.Value.TimeOfDay ?? DateTime.Now.TimeOfDay;

                bool ok = _negocio.CrearAsignacion(idChofer, idAutobus, idRuta, fecha, hora);

                if (ok)
                {
                    MessageBox.Show("Asignación creada correctamente");
                    CargarAsignaciones();
                    CargarCombos();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la asignación");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla?.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una asignación");
                    return;
                }

                int idAsignacion = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdAsignacion"].Value);

                bool ok = _negocio.FinalizarAsignacion(idAsignacion);
                if (ok)
                {
                    MessageBox.Show("Asignación finalizada");
                    CargarAsignaciones();
                    CargarCombos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ========================================
        // EVENTOS DE NAVEGACIÓN - CORREGIDOS ✅
        // ========================================

        private void pictureBox4_Click(object sender, EventArgs e) // Choferes
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

        private void pictureBox5_Click(object sender, EventArgs e) // Autobuses
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

        private void pictureBox6_Click(object sender, EventArgs e) // Rutas
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

        private void pictureBox9_Click(object sender, EventArgs e) // Usuarios
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
        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox12_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox8_Click(object sender, EventArgs e) { }
        private void pictureBox7_Click(object sender, EventArgs e) { }
        private void pictureBox15_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void pictureBox11_Click(object sender, EventArgs e) { }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void pictureBox17_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void pictureBox10_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboChofer.SelectedValue == null ||
                    comboAutobus.SelectedValue == null ||
                    comboRuta.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar chofer, autobús y ruta", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idChofer = Convert.ToInt32(comboChofer.SelectedValue);
                int idAutobus = Convert.ToInt32(comboAutobus.SelectedValue);
                int idRuta = Convert.ToInt32(comboRuta.SelectedValue);

                DateTime fecha = timerFecha?.Value.Date ?? DateTime.Today;
                TimeSpan hora = timerHora?.Value.TimeOfDay ?? DateTime.Now.TimeOfDay;

                bool resultado = _negocio.CrearAsignacion(idChofer, idAutobus, idRuta, fecha, hora);

                if (resultado)
                {
                    MessageBox.Show("Asignación creada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAsignaciones();
                    CargarCombos();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la asignación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para modificar una asignación, finalízala y crea una nueva", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTabla.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una asignación para finalizar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idAsignacion = Convert.ToInt32(dgvTabla.CurrentRow.Cells["IdAsignacion"].Value);

                var resultado = MessageBox.Show("¿Está seguro que desea finalizar esta asignación?",
                                              "Confirmar",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool finalizado = _negocio.FinalizarAsignacion(idAsignacion);

                    if (finalizado)
                    {
                        MessageBox.Show("Asignación finalizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarAsignaciones();
                        CargarCombos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo finalizar la asignación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}