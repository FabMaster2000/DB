using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data.OleDb;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DB
{
    /// <summary>
    /// Interaktionslogik für DocSite.xaml
    /// </summary>
    public partial class DocSite : Window
    {

        String state;
        int id;


        public DocSite()
        {
            InitializeComponent();
        }

        

        private void txtb_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                id = Int32.Parse(txtb_id.Text);                
            }
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            id = Int32.Parse(txtb_id.Text);
            //Edit the path to your Location - Add Datalink in VS
            String path = Environment.CurrentDirectory;
            path = path + "\\datenbanken\\coronaRadar.accdb";
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Persist Security Info=True";
            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();

                 //Test
                 OleDbCommand command = new OleDbCommand("UPDATE state SET state=3, testDate='" + DateTime.Now.ToString("dd-MM-yyyy") + " ' WHERE ID=" + id + "; ", conn);
                 command.ExecuteNonQuery();
                MessageBox.Show("Patient wurde als infiziert gemeldet.");


            }
            catch (Exception)
            {
                MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
            }

        }
    }
}
