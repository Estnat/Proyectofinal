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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ESTUARDOASUS;Initial Catalog=g6_bibliotecas;Integrated Security=True");
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string contra = textBox2.Text;
            string Bnombre = "";
            string BID = "";


            try {

                con.Open();
                SqlCommand cmd = new SqlCommand("select  usuarios.nombre, usuarios.contra, bibliotecas.nombre ,bibliotecas.id from usuarios, bibliotecas  where (usuarios.biblioteca_id=bibliotecas.id) and (usuarios.contra=@contra and  usuarios.nombre=@nombre ) ", con);

                cmd.Parameters.AddWithValue("nombre", nombre);
                cmd.Parameters.AddWithValue("contra", contra);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1) {

                    Bnombre=dt.Rows[0][2].ToString();
                    BID = dt.Rows[0][3].ToString();
                    this.Hide();

                    new MDIParent1(Bnombre,BID).Show();
                    //this.Close();
                }
                else { MessageBox.Show("Usuario y/o contraseña incorrecta"); }




            }
            catch(Exception a) { MessageBox.Show(a.Message); }
            finally { con.Close(); }
        }
    }
}
