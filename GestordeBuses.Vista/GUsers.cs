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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestordeBuses.Vista
{
    public partial class GUsers : Form
    {
        private readonly Usuario _usuario;
        private readonly UsuarioNegocio _negocio;

        public GUsers()
        {
            InitializeComponent();
            _negocio = new UsuarioNegocio();
            ConfigurarControles();
            CargarUsuarios();
        }

        public GUsers(Usuario usuario) : this()
        {
            _usuario = usuario;
        }

        private void ConfigurarControles()
        {
            if (comboRol != null)
            {
                comboRol.Items.Clear();
                comboRol.Items.AddRange(new[] { "Admin", "Usuario" });
                comboRol.SelectedIndex = 1;
                comboRol.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            if (comboEstado != null)
            {
                comboEstado.Items.Clear();
                comboEstado.Items.AddRange(new[] { "Activo", "Inactivo" });
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
                if (txtNombre != null) txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                if (txtApellidos != null) txtApellidos.Text = row.Cells["Apellido"].Value?.ToString();
                if (txtUsuario != null) txtUsuario.Text = row.Cells["Usuario"].Value?.ToString();
                if (comboRol != null) comboRol.SelectedItem = row.Cells["Rol"].Value?.ToString();
                if (comboEstado != null) comboEstado.SelectedItem = row.Cells["Estado"].Value?.ToString();
                if (txtContraseña != null) txtContraseña.Clear();
            }
            catch { }
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable dt = _negocio.ListarUsuarios();
                if (dgvTabla != null)
                    dgvTabla.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private bool ValidarFormulario()
        {
            if (txtNombre == null || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio");
                return false;
            }
            if (txtApellidos == null || string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("El apellido es obligatorio");
                return false;
            }
            if (txtUsuario == null || string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario es obligatorio");
                return false;
            }
            if (txtContraseña == null || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("La contraseña es obligatoria");
                return false;
            }
            return true;
        }

        private void LimpiarFormulario()
        {
            if (txtNombre != null) txtNombre.Clear();
            if (txtApellidos != null) txtApellidos.Clear();
            if (txtUsuario != null) txtUsuario.Clear();
            if (txtContraseña != null) txtContraseña.Clear();
            if (comboRol != null) comboRol.SelectedIndex = 1;
            if (comboEstado != null) comboEstado.SelectedIndex = 0;
        }

        // ========================================
        // EVENTOS CRUD
        // ========================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                var nuevoUsuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellidos.Text.Trim(),
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contrasena = txtContraseña.Text.Trim(),
                    Rol = comboRol.SelectedItem.ToString(),
                    Estado = comboEstado.SelectedItem.ToString()
                };

                bool ok = _negocio.CrearUsuario(nuevoUsuario);
                if (ok)
                {
                    MessageBox.Show("Usuario creado correctamente");
                    CargarUsuarios();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear usuario: " + ex.Message);
            }
        }

        // ========================================
        // EVENTOS DE NAVEGACIÓN - CORREGIDOS ✅
        // ========================================

        private void close1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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

        private void pictureBox9_Click(object sender, EventArgs e) { }
    }
}