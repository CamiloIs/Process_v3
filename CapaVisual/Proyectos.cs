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
    public partial class Proyectos : Form
    {
        public Proyectos()
        {
            InitializeComponent();
            MostrarTabla();
        }

       
        modeloProyecto man = new modeloProyecto();
        private bool Editar = false;
        private int id;
        //private string rut = "";

        MySqlConnection conectar = new MySqlConnection("Server=localhost; Database=process; User=root; port=3306; password=1994; SSL Mode=0;");
        DataSet ds;

        DataSet resultado = new DataSet();
        DataView miFiltro;

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {

                modeloProyecto man = new modeloProyecto();


                Proyecto.nombreProyecto = this.txtNombre.Text;
                Proyecto.inicio = this.dtin.Value;
                Proyecto.termino = this.dtter.Value;
                Proyecto.descripcion = this.txtDes.Text;
                Proyecto.responsable = this.txtRes.Text;
                Proyecto.empresa = this.txtEmp.Text;
                Proyecto.unidad = this.txtUni.Text;
                Proyecto.creador = this.txtCre.Text;
                man.insertProyecto();

               
                MostrarTabla();
                Limpiar();
                MessageBox.Show("Proyecto Creado Exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Proyecto no Creado " + ex.Message, " Mensaje de Sistema");
            }
        }

        private void MostrarTabla()
        {
            conectar.Open();
            MySqlCommand comm = new MySqlCommand("SELECT * FROM proyecto;", conectar);

            MySqlDataAdapter con = new MySqlDataAdapter(comm);
            ds = new DataSet();
            con.Fill(ds);
            dtProyecto.DataSource = ds.Tables[0];
            conectar.Close();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtProyecto.SelectedRows.Count == 1)
                {
                    modeloProyecto man = new modeloProyecto();
                    Proyecto.idProyecto = Convert.ToInt32(dtProyecto.CurrentRow.Cells["idProyecto"].Value.ToString());
                    man.eliminarProyecto(Proyecto.idProyecto);
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            modeloProyecto pro = new modeloProyecto();

            Proyecto.idProyecto = Convert.ToInt32(this.txtid.Text);
            Proyecto.nombreProyecto = this.txtNombre.Text;
            Proyecto.inicio = this.dtin.Value;
            Proyecto.termino = this.dtter.Value;
            Proyecto.descripcion = this.txtDes.Text;
            Proyecto.responsable = this.txtRes.Text;
            Proyecto.empresa = this.txtEmp.Text;
            Proyecto.unidad = this.txtUni.Text;
            Proyecto.creador = this.txtCre.Text;
            pro.updatePro(Proyecto.idProyecto);

            
            MostrarTabla();
            Limpiar();
            MessageBox.Show("Proyecto Actualizado Exitosamente");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dtProyecto.SelectedRows.Count == 1)
            {
                Editar = true;
                txtid.Text = dtProyecto.CurrentRow.Cells["idProyecto"].Value.ToString();
                txtNombre.Text = dtProyecto.CurrentRow.Cells["nombreProyecto"].Value.ToString();
                dtin.Value = Convert.ToDateTime(dtProyecto.CurrentRow.Cells["inicioProyecto"].Value);
                dtter.Value = Convert.ToDateTime(dtProyecto.CurrentRow.Cells["terminoProyecto"].Value);
                txtDes.Text = dtProyecto.CurrentRow.Cells["descripcionProyecto"].Value.ToString();
                txtRes.Text = dtProyecto.CurrentRow.Cells["responsableProyecto"].Value.ToString();
                txtEmp.Text = dtProyecto.CurrentRow.Cells["empresa"].Value.ToString();
                txtUni.Text = dtProyecto.CurrentRow.Cells["unidad"].Value.ToString();
                txtCre.Text = dtProyecto.CurrentRow.Cells["creadorProyecto"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor", "Mensaje de Sistema");
            }
        }


        private void Limpiar()
        {
            txtid.Clear();
            txtNombre.Clear();
            txtDes.Clear();
            txtRes.Clear();
            txtEmp.Clear();
            txtUni.Clear();
            txtCre.Clear();
        
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string salidaDatos = string.Empty;
            string[] palabras_busqueda = this.txtBuscar.Text.Split(' ');

            foreach (string palabra in palabras_busqueda)
            {
                if (salidaDatos.Length == 0)
                {
                    salidaDatos = "(nombreproyecto LIKE '%" + palabra + "%' OR descripcionProyecto LIKE '%" + palabra + "%' OR responsableProyecto LIKE '%" + palabra
                                + "%' OR unidad LIKE '%" + palabra + "%' OR empresa LIKE '%" + palabra + "%' OR creadorProyecto LIKE '%" + palabra + "%')";
                }
                else
                {
                    salidaDatos += " AND (nombreProyecto LIKE '%" + palabra + "%' OR descripcionProyecto LIKE '%" + palabra + "%' OR responsableProyecto LIKE '%" + palabra
                                + "%' OR unidad LIKE '%" + palabra + "%' OR empresa LIKE '%" + palabra + "%' OR creadorProyecto LIKE '%" + palabra + "%')";
                }
            }

            this.miFiltro.RowFilter = salidaDatos;
        }

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

        private void Proyectos_Load(object sender, EventArgs e)
        {
            this.leerDatos("SELECT * FROM proyecto; ", ref resultado, "proyecto");
            this.miFiltro = ((DataTable)resultado.Tables["proyecto"]).DefaultView;
            this.dtProyecto.DataSource = miFiltro;
        }
    }
}
