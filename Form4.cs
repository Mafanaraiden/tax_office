using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tax___
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            textBox12.Visible = false;
            textBox19.Visible = false;
            textBox18.Visible = false;
            textBox17.Visible = false;
            textBox16.Visible = false;
            textBox15.Visible = false;
            textBox11.Visible = false;
            textBox10.Visible = false;
            textBox9.Visible = false;
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

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection c = Connect();
                c.Open();

                string q1 = "insert into nalogoplatilschik values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','"
                    + textBox5.Text + "', '" + textBox8.Text + "', " + textBox7.Text + ", " + textBox6.Text + ")";
                MySqlCommand command = new MySqlCommand(q1, c); command.ExecuteNonQuery(); 
                if (checkBox4.Checked) { string query = "insert into taxes values('" + textBox1.Text + "','Земельный' ," + textBox9.Text + ")";
                    command = new MySqlCommand(query, c); command.ExecuteNonQuery();
            }
                if (checkBox5.Checked)
                {

                    string query1 = "insert into taxes values('" + textBox1.Text + "', 'Транспортный', " + textBox10.Text + ")";
                    command = new MySqlCommand(query1, c); command.ExecuteNonQuery();
                }
                if (checkBox6.Checked)
                {
                    string query2 = "insert into taxes values('" + textBox1.Text + "', 'Водный', " + textBox11.Text + ")";
                    command = new MySqlCommand(query2, c); command.ExecuteNonQuery();
                }
                if (checkBox1.Checked)
                {
                    int a = int.Parse(textBox12.Text)-int.Parse(textBox17.Text);
                    
                    string qq1 = "insert into source_income(INN, name, amount_per_mounth, amount_after_tax) values('"+textBox1.Text+"','"+checkBox1.Text+"',"+
                        textBox12.Text+","+a+")";
                    command = new MySqlCommand(qq1, c); command.ExecuteNonQuery();
                }
                
                if (checkBox2.Checked)
                {
                    int a = int.Parse(textBox15.Text) - int.Parse(textBox18.Text);

                    string qq1 = "insert into source_income(INN, name, amount_per_mounth, amount_after_tax) values('" + textBox1.Text + "','" + checkBox2.Text + "'," +
                        textBox15.Text + "," + a + ")";
                    command = new MySqlCommand(qq1, c); command.ExecuteNonQuery();
                }
                
                if (checkBox3.Checked)
                {
                    int a = int.Parse(textBox16.Text) - int.Parse(textBox19.Text);

                    string qq1 = "insert into source_income(INN, name, amount_per_mounth, amount_after_tax) values('" + textBox1.Text + "','" + checkBox3.Text + "'," +
                        textBox16.Text + "," + a + ")";
                    command = new MySqlCommand(qq1, c); command.ExecuteNonQuery();
                }
                MessageBox.Show("Данные добавлены");
                c.Close();
                this.Close();
            }
            catch(Exception q) { 
            

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox12.Visible = true;
                textBox17.Visible = true;
            }
            else
            {
                textBox12.Visible = false; textBox12.Clear();
                textBox17.Visible = false; textBox17.Clear();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox15.Visible = true;
                textBox18.Visible = true;
            }
            else
            {
                textBox15.Visible = false; textBox15.Clear();
                textBox18.Visible = false; textBox18.Clear();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox16.Visible = true;
                textBox19.Visible = true;
            }
            else
            {
                textBox16.Visible = false; textBox16.Clear(); textBox19.Clear();
                textBox19.Visible = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox9.Visible = true;
            }
            else
            {
                textBox9.Visible = false; textBox9.Clear();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                textBox10.Visible = true;
            }
            else
            {
                textBox10.Visible = false; textBox10.Clear();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox11.Visible = true;
            }
            else
            {
                textBox11.Visible = false; textBox11.Clear();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
