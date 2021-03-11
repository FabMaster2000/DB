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
       
        public MainWindow()
        {
            InitializeComponent();

            //Edit the path to your Location - Add Datalink in VS
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Documents\\coronaRadar.accdb;Persist Security Info=True";
            OleDbConnection conn = new OleDbConnection(connectionString);
           
            try
            {
                conn.Open();

                //Test
                OleDbCommand command = new OleDbCommand("SELECT testDate FROM state;", conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show(String.Format("{0}, ", reader[0]));
                }


            }
            
            catch (Exception)
            {
                MessageBox.Show("Failed to connect to data source");
            }
            
        }

        
       

        private void cmb_test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int test = cmb_test.SelectedIndex;
            switch (test)
            {
                case (0):
                    img_orange.Visibility = Visibility.Visible;

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
