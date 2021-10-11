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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            groupBox2.Visible = false;
            
        }

        int AMOUNT_TAX = 0;

        public bool Check(string ch)
        {
            bool flag = true;
            if (ch.Length != 10)
            {
                return false;
            }
            
            return flag;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Необходимо указать свои данные, вид налога и сумму", "Информационно-разъяснительная работа");
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

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection c = Connect();
            c.Open();
            MySqlCommand cmd = c.CreateCommand();
            cmd.CommandText = "select INN, passport_series, passport_number from nalogoplatilschik where INN="+textBox1.Text;
            try
            {
                bool flag = true;
                string INN = "";
                string series = "";
                string number = "";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            INN = reader.GetString(reader.GetOrdinal("INN"));
                            series = reader.GetString(reader.GetOrdinal("passport_series"));
                            number = reader.GetString(reader.GetOrdinal("passport_number"));
                            if(INN != textBox1.Text && series != textBox2.Text && number != textBox3.Text)
                            {
                                MessageBox.Show("Ваших данных в базе нет. Заполните пожалуйста свои данные");
                                Form4 f = new Form4();
                                f.ShowDialog();
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ваших данных в базе нет. Заполните пожалуйста свои данные");
                        Form4 f = new Form4();
                        f.ShowDialog();
                    }
                }
                if(INN == textBox1.Text && series == textBox2.Text && number == textBox3.Text)
                {
                    groupBox2.Visible = true;
                    cmd.CommandText = "select * from taxes where INN="+textBox1.Text;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                string tax_name = reader.GetString(reader.GetOrdinal("name"));
                                string amount = reader.GetString(reader.GetOrdinal("amount"));
                                AMOUNT_TAX += int.Parse(amount);
                                ListViewItem l = new ListViewItem(new string[] { tax_name, amount });
                                listView1.Items.Add(l);
                            }
                        }
                    }
                    cmd.CommandText = "select * from source_income where INN="+ INN;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                string amount_m = reader.GetString(reader.GetOrdinal("amount_per_mounth"));
                                string am = reader.GetString(reader.GetOrdinal("amount_after_tax"));
                                AMOUNT_TAX += (int.Parse(amount_m) - int.Parse(am));
                                ListViewItem l = new ListViewItem(new string[] { name, amount_m, am });
                                listView2.Items.Add(l);
                            }
                        }
                    }
                    
                    label5.Text = AMOUNT_TAX.ToString();
                }
                
            }
            catch (InvalidCastException ee)
            {

            }
            c.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection c = Connect();
            c.Open();
            try
            {
                
                if (!Check(textBox5.Text))
                {
                    throw new Exception();
                }
            }
            catch(Exception eee)
            {

            }
            string query = "insert into declarations(taxpayer_INN, amount_paid, nessesary_amount, start_service_date, check__) values('" + textBox1.Text+"',"+ textBox4.Text+","+label5.Text+",'"+ textBox5.Text+"','F')";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(query, c);
            // выполняем запрос
            command.ExecuteNonQuery();
            c.Close();
            MessageBox.Show("Заявка отправлена");
            
            textBox4.Clear();
            textBox5.Clear(); listView1.Clear(); listView2.Clear();
            groupBox2.Visible = false;
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear(); listView1.Clear(); listView2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(textBox1.Text, textBox2.Text, textBox3.Text);
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6(textBox1.Text, textBox2.Text, textBox3.Text);
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();

        }
    }
}
