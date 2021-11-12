using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaSoporte.cache;
using CapaNegocio;
using CapaAcessoDatos;
using MySql.Data.MySqlClient;


namespace CapaVisual
{
    public partial class panelAdmin : Form
    {
        public panelAdmin()
        {
            InitializeComponent();
            //LoadUserData();
            //man.listarUsuarios();
            //MostrarCuenta();
            cbxRol.DataSource = Enum.GetValues(typeof(EnumRol));
            //txtid.Text = dtcargo.CurrentRow.Cells[0].Value.ToString();
            cbxregion.DataSource = Enum.GetValues(typeof(enuReg));
            cbxcomuna.DataSource = Enum.GetValues(typeof(comuna));
            
        }
        modeloUsuario man = new modeloUsuario();
        private bool Editar = false;
        private int id;
        //private string rut = "";

        MySqlConnection conectar = new MySqlConnection("Server=localhost; Database=process; User=root; port=3306; password=1994; SSL Mode=0;");
        DataSet ds;

        DataSet resultado = new DataSet();
        DataView miFiltro;

        public void combo()
        {
            if (cbxregion.SelectedIndex == 1)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna1));
            }

            if (cbxregion.SelectedIndex == 2)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna2));
            }

            if (cbxregion.SelectedIndex == 3)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna3));
            }

            if (cbxregion.SelectedIndex == 4)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna4));
            }

            if (cbxregion.SelectedIndex == 5)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna5));
            }

            if (cbxregion.SelectedIndex == 6)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna6));
            }

            if (cbxregion.SelectedIndex == 7)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna7));
            }

            if (cbxregion.SelectedIndex == 8)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna8));
            }

            if (cbxregion.SelectedIndex == 9)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna9));
            }

            if (cbxregion.SelectedIndex == 10)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna10));
            }

            if (cbxregion.SelectedIndex == 11)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna11));
            }

            if (cbxregion.SelectedIndex == 12)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna12));
            }

            if (cbxregion.SelectedIndex == 13)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna13));
            }
            if (cbxregion.SelectedIndex == 14)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna14));
            }
            if (cbxregion.SelectedIndex == 15)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna15));
            }
            if (cbxregion.SelectedIndex == 16)
            {
                cbxcomuna.DataSource = Enum.GetValues(typeof(comuna16));
            }
        }

        private void MostrarTabla()
        {
            conectar.Open();
            MySqlCommand comm = new MySqlCommand("SELECT u.idusuario, u.rut, u.nombre,u.apellidos, u.correo, u.telefono, u.region, u.comuna, " +
                                                 "c.usuario, c.password, u.cargo, c.rol FROM usuario u JOIN cuenta c ON u.idusuario = c.idcuenta; ", conectar);

            MySqlDataAdapter con = new MySqlDataAdapter(comm);
            ds = new DataSet();
            con.Fill(ds);
            dtUsuarios.DataSource = ds.Tables[0];
            conectar.Close();

        }

        private void MostrarId()
        {
            conectar.Open();
            MySqlCommand comm = new MySqlCommand("select max(idusuario) as id from usuario", conectar);

            MySqlDataAdapter con = new MySqlDataAdapter(comm);
            ds = new DataSet();
            con.Fill(ds);
            dtcargo.DataSource = ds.Tables[0];
            conectar.Close();
            


        }

        //private void MostrarCuenta()
        //{
        //    conectar.Open();
        //    MySqlCommand comm = new MySqlCommand("SELECT * FROM cuenta", conectar);

        //    MySqlDataAdapter con = new MySqlDataAdapter(comm);
        //    ds = new DataSet();
        //    con.Fill(ds);
        //    dtcc.DataSource = ds.Tables[0];
        //    conectar.Close();

        //    //CapaAcessoDatos.conexionMySQL con = new conexionMySQL();
        //    //con.Open();
        //    //modeloUsuario man = new modeloUsuario();
        //    ////dtUsuarios.DataSource = man.listarUsuarios();
        //    ////dtUsuarios.Tables[0];
        //    ////dtUsuarios.DataMember = "usuario";
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to log out?", "Warning",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Dispose();
        }

        private void LoadUserData()
        {
            lblUser.Text = cacheUsuario.nombre;
            lblRol.Text = cuenta.rol;
            lblCorreo.Text = cacheUsuario.correo;
        }

        private void panelAdmin_Load(object sender, EventArgs e)
        {
            this.leerDatos("SELECT u.idusuario, u.rut, u.nombre, u.apellidos, u.correo, u.telefono, u.region, u.comuna, c.usuario, c.password, u.cargo, c.rol " +
                           "FROM usuario u JOIN cuenta c ON u.idusuario = c.idcuenta; ", ref resultado, "usuario");
            this.miFiltro = ((DataTable)resultado.Tables["usuario"]).DefaultView;
            this.dtUsuarios.DataSource = miFiltro;
      
        }

        
    
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                
                modeloUsuario man = new modeloUsuario();

                Usuario.rut = this.txtRut.Text;
                Usuario.nombre = this.txtNombre.Text;
                Usuario.apellidos = this.txtApellido.Text;
                Usuario.correo = this.txtCorreo.Text;
                Usuario.telefono = Convert.ToInt32(this.txtTelefono.Text);
                Usuario.region = this.cbxregion.SelectedItem.ToString();
                Usuario.comuna = this.cbxcomuna.SelectedItem.ToString();
                Usuario.usuario = this.txtUser.Text;
                Usuario.cargo = this.txtCargo.Text;
                man.insert();
                
                id = man.idUser(Usuario.idUsuario);
                MostrarId();
                MostrarTabla();
                txtid.Text = dtcargo.CurrentRow.Cells[0].Value.ToString();


                cuenta.idCuenta = Convert.ToInt32(this.txtid.Text);
                cuenta.usuario = this.txtUser.Text;
                cuenta.password = this.txtPass.Text;
                cuenta.rol = this.cbxRol.SelectedItem.ToString();
                man.insertCuenta();
                MostrarTabla();
                //MostrarCuenta();
               

                Limpiar();
                MessageBox.Show("Usuario Creado Exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario no Creado" + ex.Message, "Mensaje de Sistema");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
                modeloUsuario man = new modeloUsuario();


                Usuario.idUsuario = Convert.ToInt32(txtid.Text);
                Usuario.rut = txtRut.Text;
                Usuario.nombre = txtNombre.Text;
                Usuario.apellidos = txtApellido.Text;
                Usuario.correo = txtCorreo.Text;
                Usuario.telefono = Convert.ToInt32(txtTelefono.Text);
                Usuario.region = cbxregion.SelectedItem.ToString();
                Usuario.comuna = cbxcomuna.SelectedItem.ToString();
                Usuario.usuario = txtUser.Text;
                Usuario.cargo = txtCargo.Text;


                cuenta.idCuenta = Convert.ToInt32(this.txtid.Text);
                cuenta.usuario = this.txtUser.Text;
                cuenta.password = this.txtPass.Text;
                cuenta.rol = this.cbxRol.SelectedItem.ToString(); 
                man.updateUser(Usuario.rut);
                man.updateCuenta(cuenta.usuario);
                MostrarTabla();
                Limpiar();
                MessageBox.Show("Usuario Actualizado Exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario no Actualizado" + ex.Message, "Mensaje de Sistema");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtUsuarios.SelectedRows.Count == 1)
                {
                    modeloUsuario man = new modeloUsuario();
                    Usuario.idUsuario = Convert.ToInt32(dtUsuarios.CurrentRow.Cells["idusuario"].Value.ToString());
                    man.eliminarUser(id);
                    MostrarTabla();
                    Limpiar();

                    

                    MessageBox.Show("Eliminado correctamente", "Mensaje de Sistema");
                    
                }
                else
                    MessageBox.Show("Seleccione una fila por favor", "Mensaje de Sistema");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        //private void leerDatos(/*string query, ref DataSet dtsprincipal, string tabla*/)
        //{
        //    try
        //    {
        //        modeloUsuario man = new modeloUsuario();
        //        man.insert();

        //        //string cadena = "Data Source=DESKTOP-SVH5M1U;Initial Catalog=evaGames;Integrated Security=True";
        //        //MySqlConnection cn = new SqlConnection(cadena);
        //        //SqlCommand cmd = new SqlCommand(query, cn);
        //        //cn.Open();
        //        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        //da.Fill(dtsprincipal, tabla);
        //        //da.Dispose();
        //        //cn.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }


        //}

        private void leerDatos(string query, ref DataSet dtsprincipal, string tabla)
        {
            try
            {
                string cadena = "Server = localhost; Database = process; User = root; port = 3306; password =1994; SSL Mode = 0; ";
                MySqlConnection cn = new MySqlConnection(cadena);
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtsprincipal, tabla);
                da.Dispose();
                cn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string salidaDatos = string.Empty;
            string[] palabras_busqueda = this.txtBuscar.Text.Split(' ');

            foreach (string palabra in palabras_busqueda)
            {
                if(salidaDatos.Length == 0)
                {
                    salidaDatos = "(nombre LIKE '%" + palabra + "%' OR apellidos LIKE '%" + palabra + "%' OR rut LIKE '%" + palabra
                                + "%' OR usuario LIKE '%" + palabra + "%' OR correo LIKE '%" + palabra + "%' OR rol LIKE '%" + palabra + "%')";
                }
                else
                {
                    salidaDatos += " AND (nombre LIKE '%" + palabra + "%' OR apellidos LIKE '%" + palabra + "%' OR rut LIKE '%" + palabra
                                + "%' OR usuario LIKE '%" + palabra + "%' OR correo LIKE '%" + palabra + "%' OR rol LIKE '%" + palabra + "%')";
                }
            }

            this.miFiltro.RowFilter = salidaDatos;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dtUsuarios.SelectedRows.Count == 1)
            {
                Editar = true;
                txtid.Text = dtUsuarios.CurrentRow.Cells["idusuario"].Value.ToString();
                txtRut.Text = dtUsuarios.CurrentRow.Cells["rut"].Value.ToString();
                txtNombre.Text = dtUsuarios.CurrentRow.Cells["nombre"].Value.ToString();
                txtApellido.Text = dtUsuarios.CurrentRow.Cells["apellidos"].Value.ToString();
                txtCorreo.Text = dtUsuarios.CurrentRow.Cells["correo"].Value.ToString();
                txtTelefono.Text = dtUsuarios.CurrentRow.Cells["telefono"].Value.ToString();
                cbxregion.Text = dtUsuarios.CurrentRow.Cells["region"].Value.ToString();
                cbxcomuna.Text = dtUsuarios.CurrentRow.Cells["comuna"].Value.ToString();
                txtUser.Text = dtUsuarios.CurrentRow.Cells["usuario"].Value.ToString();
                txtPass.Text = dtUsuarios.CurrentRow.Cells["password"].Value.ToString();
                txtCargo.Text = dtUsuarios.CurrentRow.Cells["cargo"].Value.ToString();
                cbxRol.Text = dtUsuarios.CurrentRow.Cells["rol"].Value.ToString();




                //txtRol.Text = dtUsuarios.CurrentRow.Cells["rol"].Value.ToString();
                //txtid.Text = dtUsuarios.CurrentRow.Cells["jerarquia"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor", "Mensaje de Sistema");
            }
        }

        

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void Limpiar()
        {
            txtid.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtRut.Clear();
            txtUser.Clear();
            txtCorreo.Clear();
            cbxRol.SelectedIndex = 0;
            txtPass.Clear();
            cbxregion.SelectedIndex = 0;
            cbxcomuna.SelectedIndex = 0;
            txtPass.Clear();
            txtCargo.Clear();
            txtTelefono.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dtcargo_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
           
        }

        private void cbxregion_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo();
        }
    }
}
