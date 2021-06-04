using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyectofinal
{
    class CargarItemsCombobox
    {
        SqlConnection con = new SqlConnection(@"Data Source=ESTUARDOASUS;Initial Catalog=g6_bibliotecas;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

        public CargarItemsCombobox()
        {
            try { }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        
        
        
        }

        public void Llenaritems(ComboBox cb,string posicion) {

            try {
                con.Open();
                cmd = new SqlCommand("select id, nombre from tipos_articulos  ", con);
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    cb.Items.Add(dr["nombre"].ToString());
                    if (dr["nombre"].ToString()==posicion) { i = Int32.Parse(dr["id"].ToString()); }
                    
                }
                cb.SelectedIndex = i-1;
                dr.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }



        }



    }
}
