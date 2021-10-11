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
    public partial class Form6 : Form
    {

        string dec_num = "";
        ListViewItem index = new ListViewItem();
        public Form6(string inn, string ser, string num)
        {
            InitializeComponent();
            MySqlConnection c = Connect();
            c.Open();
            MySqlCommand cmd = c.CreateCommand();
            cmd.CommandText = "select nessesary_amount, amount_paid, num_declaration from declarations where taxpayer_INN='" + inn + "' and check__='t'";
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string am1 = reader.GetString(reader.GetOrdinal("nessesary_amount"));
                        string am2 = reader.GetString(reader.GetOrdinal("amount_paid"));
                        dec_num = reader.GetString(reader.GetOrdinal("num_declaration"));
                        int owe = int.Parse(am1) - int.Parse(am2);
                        string owe_str = owe.ToString();
                        ListViewItem l = new ListViewItem(new string[] { inn, owe_str, dec_num });
                        listView1.Items.Add(l);
                    }
                }
            }
            c.Close();
            label1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;

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
        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            MySqlConnection c = Connect();
            c.Open();
            string q1 = "update declarations set amount_paid=amount_paid+" + textBox1.Text+ " where num_declaration = " + dec_num;
            MySqlCommand command = new MySqlCommand(q1, c); command.ExecuteNonQuery();
            c.Close();
            textBox1.Clear();
            textBox1.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            listView1.Items.Remove(index);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dec_num = listView1.FocusedItem.SubItems[2].Text;
            index = listView1.FocusedItem;
            label1.Visible = true;
            button1.Visible = true;
            textBox1.Visible = true;
        }
    }
}
