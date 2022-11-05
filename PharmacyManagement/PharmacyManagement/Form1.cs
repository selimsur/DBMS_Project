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

namespace PharmacyManagement
{
    public partial class Form1 : Form
    {
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader dataReader;

        public Form1()
        {
            InitializeComponent();

            connection = new MySqlConnection("Server=localhost;Database=pharmacymanagementsystem;user=root;Pwd=;SslMode=none");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string password = textBox2.Text;
            Form f2 = new Form2();

            command = new MySqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = " SELECT * FROM users where tc='" + textBox1.Text + "' AND password='" + textBox2.Text + "' AND id='1' ";
            dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                f2.Show();
                connection.Close();
            }
            else
            {
                connection.Close();
                connection.Open();
                command.Connection = connection;
                command.CommandText = " SELECT * FROM users where tc='" + textBox1.Text + "' AND password='" + textBox2.Text + "' ";
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    MessageBox.Show("user");
                }
            }
            connection.Close();
            

        }
    }
}
