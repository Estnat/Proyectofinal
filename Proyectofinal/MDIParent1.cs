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

namespace Proyectofinal
{
    public partial class MDIParent1 : Form
    {
        string BIDG = "";

        private int childFormNumber = 0;
        SqlConnection con = new SqlConnection(@"Data Source=ESTUARDOASUS;Initial Catalog=g6_bibliotecas;Integrated Security=True");
        public MDIParent1(string Bnombre,string BID)
        {
            this.Text = Bnombre;
            InitializeComponent();
            this.Text = Bnombre;
            BIDG = BID;
            Llenargrid(BIDG);
        }

        public void Llenargrid(string BID)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(" SELECT articulos.id, articulos.nombre ,articulos.publicacion ,articulos.isbn ,articulos.costo_alquiler, tipos_articulos.nombre, articulos.biblioteca_id  FROM articulos, tipos_articulos, bibliotecas where articulos.tipo_articulo_id = tipos_articulos.id and bibliotecas.id = articulos.biblioteca_id and bibliotecas.id = 1 ", con);
                        
           // cmd.Parameters.AddWithValue("BID", BID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[4].Value.ToString();
            textBox6.Text = dataGridView1.SelectedCells[5].Value.ToString();
            CargarItemsCombobox c = new CargarItemsCombobox();
            c.Llenaritems(comboBox1, textBox6.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int inde = comboBox1.SelectedIndex+1;
            string cambio = textBox5.Text;
            string cambias = cambio.Replace(",",".");
            try
            {
                con.Open();
            string ingresar = "insert into articulos (nombre, publicacion, isbn, costo_alquiler, biblioteca_id, tipo_articulo_id) "
                + "values( @nombre, @publicacion, " + textBox4.Text+" , "+ cambias + " , "+ BIDG+ " , "+ inde +")";

            //MessageBox.Show(ingresar);
            SqlCommand comando = new SqlCommand(ingresar, con);
            comando.Parameters.AddWithValue("nombre", textBox2.Text);
            comando.Parameters.AddWithValue("publicacion", textBox3.Text);

            comando.ExecuteNonQuery();
            MessageBox.Show("Registro agregado");
            }
            catch (Exception a) { MessageBox.Show(a.Message); }
            finally { con.Close(); }

            Llenargrid(BIDG);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int inde = comboBox1.SelectedIndex + 1;
            string cambio = textBox5.Text;
            string cambias = cambio.Replace(",", ".");
            string v = textBox1.Text.ToString();
            try
            {
                con.Open();
                
                string ingresar = "UPDATE articulos Set nombre = @nombre , publicacion = @publicacion , isbn =  " + textBox4.Text
                    + ", costo_alquiler = " + cambias + " ,  biblioteca_id= " + BIDG + "  where id=" + v;

                //MessageBox.Show(ingresar);
                SqlCommand comando = new SqlCommand(ingresar, con);
                comando.Parameters.AddWithValue("nombre", textBox2.Text);
                comando.Parameters.AddWithValue("publicacion", textBox3.Text);

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro modificado");
            }
            catch (Exception a) { MessageBox.Show(a.Message); }
            finally { con.Close(); }

            Llenargrid(BIDG);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int inde = comboBox1.SelectedIndex + 1;
            string cambio = textBox5.Text;
            string cambias = cambio.Replace(",", ".");
            string v = textBox1.Text.ToString();
            try
            {
                con.Open();

                string ingresar = "DELETE FROM articulos where id=" + v;

                //MessageBox.Show(ingresar);
                SqlCommand comando = new SqlCommand(ingresar, con);
                comando.Parameters.AddWithValue("nombre", textBox2.Text);
                comando.Parameters.AddWithValue("publicacion", textBox3.Text);

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado");
            }
            catch (Exception a) { MessageBox.Show(a.Message); }
            finally { con.Close(); }

            Llenargrid(BIDG);


        }
    }
    
}
