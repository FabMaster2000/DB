using System;
using System.Windows;
using System.Windows.Input;
using System.Data.OleDb;
using System.Collections.Generic;

namespace DB
{
    /// <summary>
    /// Interaktionslogik für DocSite.xaml
    /// </summary>
    public partial class DocSite : Window
    {

        int id;
        String ids;
        List<int> contacts = new List<int>();


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

                OleDbCommand command = new OleDbCommand("SELECT ID FROM [user] WHERE ID IN (SELECT UserID1 FROM [events] WHERE UserID2 = " + id + ")" +
                    " OR ID IN (SELECT UserID2 FROM [events] WHERE UserID1=" + id + ")", conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ids = String.Format("{0}, ", reader[0]);
                    ids = ids.Substring(0, ids.Length - 2);
                    int a = Int32.Parse(ids);
                    contacts.Add(a);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            foreach(int c in contacts)
            {
                // inform other imidiate Users
                try
                {
                    conn.Open();
                    OleDbCommand command = new OleDbCommand("INSERT INTO [state] (UserID, testDate, state) Values " +
                        "(" + c + ", '" + DateTime.Now.ToString("dd-MM-yyyy") + " ', 2)", conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
                }
            }

            

            // set as infected
            try
            {
                conn.Open();

                OleDbCommand command = new OleDbCommand("INSERT INTO [state] (UserID, testDate, state) Values " +
                        "(" + id + ", '" + DateTime.Now.ToString("dd-MM-yyyy") + " ', 3)", conn);
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
