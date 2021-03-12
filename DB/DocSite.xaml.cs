using System;
using System.Windows;
using System.Windows.Input;
using System.Data.OleDb;
namespace DB
{
    /// <summary>
    /// Interaktionslogik für DocSite.xaml
    /// </summary>
    public partial class DocSite : Window
    {

        int id;
        int[] contacts;
        String ids;
        int i = 0;


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

            // get all contacts from infected user
            try
            {
                conn.Open();

                //Test
                OleDbCommand command = new OleDbCommand("SELECT UserID1, UserID2 from events WHERE UserID1 = " + id + " OR UserID2 = " + id + "; ", conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ids = String.Format("{0}, ", reader[0]);
                    ids = ids.Substring(0, ids.Length - 2);
                }
                contacts[i] = Int32.Parse(ids);
                i++;
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
            }

            // inform other second grade Users
            try
            {
                conn.Open();
                foreach (int user in contacts)
                {
                    OleDbCommand command = new OleDbCommand("UPDATE state SET state=1 WHERE UserID=" +
                    "(SELECT UserID1, UserID2 from events WHERE UserID1=" + user + " OR UserID2=" + user + "); ", conn);
                command.ExecuteNonQuery();        
                }
                conn.Close();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
            }


            // inform other imidiate Users
            try
            {
                conn.Open();

                //Test
                OleDbCommand command = new OleDbCommand("UPDATE state SET state=2 WHERE UserID=" +
                    "(SELECT UserID1, UserID2 from events WHERE UserID1=" + id + " OR UserID2=" + id + "); ", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
            }

            // set as infected
            try
            {
                conn.Open();

                //Test
                OleDbCommand command = new OleDbCommand("UPDATE state SET state=3, testDate='" + DateTime.Now.ToString("dd-MM-yyyy") + 
                    " ' WHERE UserID=" + id + "; ", conn);
                command.ExecuteNonQuery();
                MessageBox.Show("Patient wurde als infiziert gemeldet.");
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
            }

            
        }
    }
}
