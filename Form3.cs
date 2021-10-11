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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            MySqlConnection c = Connect();
            MySqlCommand cmd = c.CreateCommand();
            c.Open();
            List<string> insp= new List<string>();
            cmd.CommandText = "select id_inspector from workers";
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string surname = reader.GetString(reader.GetOrdinal("id_inspector"));
                        insp.Add(surname);
                    }
                }
            }
            c.Close();

            comboBox1.DataSource = insp;
        }
        ListViewItem index = new ListViewItem();
        string ID_dec = "";
        string ID_worker = "";

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

        private void Form3_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button3.Visible = false;
            comboBox1.Visible = false;
            textBox1.Visible = false;
            MySqlConnection c = Connect();
            MySqlCommand cmd = c.CreateCommand();
            c.Open();
            cmd.CommandText = "select taxpayer_INN, nessesary_amount, amount_paid, num_declaration, check__ from declarations where end_service_date IS NULL";
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string inn = reader.GetString(reader.GetOrdinal("taxpayer_INN"));
                        string am1 = reader.GetString(reader.GetOrdinal("nessesary_amount"));
                        string am2 = reader.GetString(reader.GetOrdinal("amount_paid"));
                        string ch__ = reader.GetString(reader.GetOrdinal("check__"));
                        string a_chh = "";
                        if(ch__ == "t")
                        {
                            a_chh = "Долг";
                        }
                        else
                        {
                            a_chh = "";
                        }
                        ID_dec = reader.GetString(reader.GetOrdinal("num_declaration"));
                        ListViewItem l = new ListViewItem(new string[] { inn , am2, am1, ID_dec, a_chh });
                        listView1.Items.Add(l);
                    }
                }
            }
            c.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            index = listView1.FocusedItem;
            ID_dec=listView1.FocusedItem.SubItems[3].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            button3.Visible = true;

            comboBox1.Visible = true;
            textBox1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection c = Connect();
            c.Open();
            button3.Visible = false;
            string q1 = "update declarations set end_service_date= '"+textBox1.Text+"',id_inspector="+ID_worker+", check__='F' where num_declaration="+ID_dec;
            MySqlCommand command = new MySqlCommand(q1, c); command.ExecuteNonQuery();
            c.Close();
            MessageBox.Show("Запись добавлена");
            textBox1.Clear();
            listView1.Items.Remove(index);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID_worker = comboBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection c = Connect();
            c.Open();
            listView1.Items.Remove(index);
            string q1 = "update declarations set check__ = 't' where num_declaration="+ID_dec;
            MySqlCommand command = new MySqlCommand(q1, c); command.ExecuteNonQuery();
            c.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
        }
    }
}
