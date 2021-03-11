using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.OleDb;

namespace DB_projekt
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String state;

        public MainWindow()
        {
            InitializeComponent();

            //Edit the path to your Location - Add Datalink in VS
            String path = Environment.CurrentDirectory;
            path = path + "\\datenbanken\\coronaRadar.accdb";
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Persist Security Info=True";
            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();

                //Test
                OleDbCommand command = new OleDbCommand("SELECT state FROM state WHERE ID=8 ;", conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    state = String.Format("{0}, ", reader[0]);
                    state = state.Substring(0, state.Length-2);
                }
            }            
            catch (Exception)
            {
                MessageBox.Show("Failed to connect to data source");
            }

            int test = Int32.Parse(state);

            switch (test)
            {
                case (0):
                    img_green.Visibility = Visibility.Visible;
                    img_orange.Visibility = Visibility.Collapsed;
                    img_red.Visibility = Visibility.Collapsed;
                    lbl_ok.Visibility = Visibility.Visible;
                    txt_danger.Visibility = Visibility.Collapsed;
                    txt_warning.Visibility = Visibility.Collapsed;
                    break;

                case (1):
                    img_green.Visibility = Visibility.Collapsed;
                    img_orange.Visibility = Visibility.Visible;
                    img_red.Visibility = Visibility.Collapsed;
                    lbl_ok.Visibility = Visibility.Collapsed;
                    txt_danger.Visibility = Visibility.Collapsed;
                    txt_warning.Visibility = Visibility.Visible;
                    break;

                case (2):
                    img_green.Visibility = Visibility.Collapsed;
                    img_orange.Visibility = Visibility.Collapsed;
                    img_red.Visibility = Visibility.Visible;
                    lbl_ok.Visibility = Visibility.Collapsed;
                    txt_danger.Visibility = Visibility.Visible;
                    txt_warning.Visibility = Visibility.Collapsed;
                    break;
            }



        }

        
       

        private void cmb_test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int test = cmb_test.SelectedIndex;

            switch (test)
            {
                case (0):
                    img_green.Visibility = Visibility.Visible;
                    img_orange.Visibility = Visibility.Collapsed;
                    img_red.Visibility = Visibility.Collapsed;
                    lbl_ok.Visibility = Visibility.Visible;
                    txt_danger.Visibility = Visibility.Collapsed;
                    txt_warning.Visibility = Visibility.Collapsed;
                    break;

                case (1):
                    img_green.Visibility = Visibility.Collapsed;
                    img_orange.Visibility = Visibility.Visible;
                    img_red.Visibility = Visibility.Collapsed;
                    lbl_ok.Visibility = Visibility.Collapsed;
                    txt_danger.Visibility = Visibility.Collapsed;
                    txt_warning.Visibility = Visibility.Visible;
                    break;

                case (2):
                    img_green.Visibility = Visibility.Collapsed;
                    img_orange.Visibility = Visibility.Collapsed;
                    img_red.Visibility = Visibility.Visible;
                    lbl_ok.Visibility = Visibility.Collapsed;
                    txt_danger.Visibility = Visibility.Visible;
                    txt_warning.Visibility = Visibility.Collapsed;
                    break;
            }
        }

    }
}
