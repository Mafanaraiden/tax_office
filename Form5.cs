using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tax___
{
    public partial class Form5 : Form
    {
        public Form5(string inn, string series, string number)
        {
            InitializeComponent();
            label2.Text = inn;
            MySqlConnection c = Connect();
            MySqlCommand cmd = c.CreateCommand();
            c.Open();
            cmd.CommandText = "select d.*, w.id_inspector, w.surname from declarations d, workers w where d.taxpayer_inn = '" +inn+ "'and d.id_inspector = w.id_inspector";


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string am1 = reader.GetString(reader.GetOrdinal("nessesary_amount"));
                        string am2 = reader.GetString(reader.GetOrdinal("amount_paid"));
                        string surname="";
                        int aaa = reader.GetOrdinal("surname");
                        if (reader.IsDBNull(aaa))
                        {
                           surname = "-";
                        }
                        else
                        {
                            surname = reader.GetString(aaa);
                        }
                        string date1 = reader.GetString(reader.GetOrdinal("start_service_date"));
                        string date2;
                        int qqqq = reader.GetOrdinal("end_service_date");
                        if (reader.IsDBNull(qqqq))
                        {
                            date2 = "-";
                        }
                        else
                        {
                            date2 = reader.GetString(qqqq);
                        }
                        string ch1 = reader.GetString(reader.GetOrdinal("check__"));
                        string ch2="";
                        if(ch1 == "F" && date2 == "-")
                        {
                            ch2 = "";
                        }
                        else
                        {
                            ch2 = "v";
                        }
                        ListViewItem l = new ListViewItem(new string[] {am2, am1 , surname , date1, date2, ch2 });
                        listView1.Items.Add(l);
                    }
                }
            }
            c.Close();
        }
        public MySqlConnection Connect()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "tax";
            string username = "q";
            string password = "1111";
            String connString = "Server=" + host + ";Database=" + database
               + ";port=" + port + ";User Id=" + username + ";password=" + password;
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
