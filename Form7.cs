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
    public partial class Form7 : Form
    {
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
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = false;
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                
                textBox1.Visible = true;
                button1.Visible = true;
                label1.Text = "ИНН";
                label1.Visible = true; 
            }
            if (!radioButton2.Checked)
            {
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                textBox1.Clear();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label1.Visible = true;
                textBox1.Visible = true;
                button1.Visible = true;
                label1.Text = "Фамилия";
                
            }
            if (!radioButton1.Checked)
            {
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                listView1.Items.Clear();
                string srch = textBox1.Text;
                MySqlConnection c = Connect();
                MySqlCommand cmd = c.CreateCommand();
                c.Open();
                cmd.CommandText = "select * from nalogoplatilschik where surname like '%" + srch + "%'";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            string s = reader.GetString(reader.GetOrdinal("surname")); columnHeader1.Text = "Фамилия";
                            string s1 = reader.GetString(reader.GetOrdinal("INN")); columnHeader2.Text = "INN";
                            string s2 = reader.GetString(reader.GetOrdinal("passport_series")); columnHeader3.Text = "passport_series";
                            string s3 = reader.GetString(reader.GetOrdinal("passport_number")); columnHeader4.Text = "passport_number";
                            string s4 = reader.GetString(reader.GetOrdinal("birthday")); columnHeader5.Text = "birthday";
                            string s5 = reader.GetString(reader.GetOrdinal("birth_town")); columnHeader6.Text = "birth_town";
                            columnHeader7.Text = ""; columnHeader8.Text = "";
                            ListViewItem l = new ListViewItem(new string[] {s, s1, s2, s3, s4, s5 });
                            listView1.Items.Add(l);
                        }
                    }
                }
                c.Close();
            }
            if (radioButton2.Checked)
            {
                listView1.Items.Clear();
                string srch1 = textBox1.Text;
                listView1.Items.Clear();
                MySqlConnection c = Connect();
                MySqlCommand cmd = c.CreateCommand();
                c.Open();
                cmd.CommandText = "select n.INN, n.surname as sur, n.passport_series, n.passport_number, taxes.name, taxes.amount, s.name as s_name, s.amount_per_mounth as s_am from nalogoplatilschik n join taxes on n.INN=taxes.inn join source_income s on s.inn = n.inn where n.inn='"+ srch1+"'";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            
                            string s1 = reader.GetString(reader.GetOrdinal("INN")); columnHeader1.Text = "INN";
                            string s2 = reader.GetString(reader.GetOrdinal("sur")); columnHeader2.Text = "фамилия";
                            string s3 = reader.GetString(reader.GetOrdinal("passport_series")); columnHeader3.Text = "серия паспорта";
                            string s4 = reader.GetString(reader.GetOrdinal("passport_number")); columnHeader4.Text = "номер паспорта";
                            string s5 = reader.GetString(reader.GetOrdinal("name")); columnHeader5.Text = "название налога";
                            string s6 = reader.GetString(reader.GetOrdinal("amount")); columnHeader6.Text = "сумма налога";
                            string s7 = reader.GetString(reader.GetOrdinal("s_name")); columnHeader7.Text = "название дохода";
                            string s8 = reader.GetString(reader.GetOrdinal("s_am")); columnHeader8.Text = "доход";
                            ListViewItem l = new ListViewItem(new string[] { s1, s2, s3, s4, s5,s6,s7,s8 });
                            listView1.Items.Add(l);
                        }
                    }
                }
                c.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
