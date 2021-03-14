﻿using System;
using System.Windows;
using System.Data.OleDb;
using DB;

namespace DB_projekt
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String state;
        int id;
        bool fail;

        public MainWindow()
        {
            InitializeComponent();
            DocSite ds = new DocSite();
            ds.Show();
        }     

        private void lbl_id_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {

                id = Int32.Parse(lbl_id.Text);

                //Edit the path to your Location - Add Datalink in VS
                String path = Environment.CurrentDirectory;
                path = path + "\\datenbanken\\coronaRadar.accdb";
                String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Persist Security Info=True";
                OleDbConnection conn = new OleDbConnection(connectionString);

                try
                {
                    try
                    {
                        conn.Open();
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Verbindung zum Server fehlgeschlagen. Bitte versuchen Sie es später erneut.");
                        fail = true;
                    }

                    OleDbCommand command = new OleDbCommand("SELECT MAX(state) FROM state WHERE UserID=" + id + "; ", conn);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        state = String.Format("{0}, ", reader[0]);
                        state = state.Substring(0, state.Length - 2);
                    }
                    if (state.Equals("")) throw new Exception("equalls null");
                }
                catch (Exception)
                {
                    try
                    {

                        OleDbCommand command = new OleDbCommand("Select MAX(state)-1 FROM state WHERE  UserID in " +
                                                            "(SELECT UserID2 FROM events WHERE UserID1 = " + id + ")" +
                                                            "Or UserID in (Select UserID1 From events Where UserID2 = " + id + ")", conn);
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            state = String.Format("{0}, ", reader[0]);
                            state = state.Substring(0, state.Length - 2);
                        }
                        if (state.Equals("")) throw new Exception("equalls null");
                    }
                    catch (Exception)
                    {
                        if (!fail)
                        {
                            state = "0";
                        }
                        state = "4";

                    }
                }

                    int test = Int32.Parse(state);

                switch (test)
                {
                    case (0): //ok
                        img_green.Visibility = Visibility.Visible;
                        img_orange.Visibility = Visibility.Collapsed;
                        img_red.Visibility = Visibility.Collapsed;
                        lbl_ok.Visibility = Visibility.Visible;
                        txt_danger.Visibility = Visibility.Collapsed;
                        txt_warning.Visibility = Visibility.Collapsed;
                        txt_infected.Visibility = Visibility.Collapsed;
                        break;

                    case (1): // warning
                        img_green.Visibility = Visibility.Collapsed;
                        img_orange.Visibility = Visibility.Visible;
                        img_red.Visibility = Visibility.Collapsed;
                        lbl_ok.Visibility = Visibility.Collapsed;
                        txt_danger.Visibility = Visibility.Collapsed;
                        txt_warning.Visibility = Visibility.Visible;
                        txt_infected.Visibility = Visibility.Collapsed;
                        break;

                    case (2): // danger
                        img_green.Visibility = Visibility.Collapsed;
                        img_orange.Visibility = Visibility.Collapsed;
                        img_red.Visibility = Visibility.Visible;
                        lbl_ok.Visibility = Visibility.Collapsed;
                        txt_danger.Visibility = Visibility.Visible;
                        txt_warning.Visibility = Visibility.Collapsed;
                        txt_infected.Visibility = Visibility.Collapsed;
                        break;

                    case (3): // infected
                        img_green.Visibility = Visibility.Collapsed;
                        img_orange.Visibility = Visibility.Collapsed;
                        img_red.Visibility = Visibility.Visible;
                        lbl_ok.Visibility = Visibility.Collapsed;
                        txt_danger.Visibility = Visibility.Collapsed;
                        txt_warning.Visibility = Visibility.Collapsed;
                        txt_infected.Visibility = Visibility.Visible;
                        break;

                    case (4): // no internet

                        break;
                }
            }

        }
    }
}
