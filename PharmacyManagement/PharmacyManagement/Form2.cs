using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagement
{
    public partial class Form2 : Form
    {
        MySqlConnection connection;
        MySqlCommand command;
        string connectionString = "Server=localhost;Database=pharmacymanagementsystem;user=root;Pwd=;SslMode=none";
        byte[] binaryData;

        public Form2()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            userLoad();
            medicineLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            binaryData = File.ReadAllBytes(openFileDialog1.FileName);
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            textBox3.Text = openFileDialog1.FileName;
        }

        private void userLoad()
        {
            connection.Open();
            command = new MySqlCommand("SELECT * FROM users", connection);
            command.CommandType = CommandType.Text;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void medicineLoad()
        {
            connection.Open();
            command = new MySqlCommand("SELECT * FROM ilaclar", connection);
            command.CommandType = CommandType.Text;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView3.DataSource = dataTable;
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new MySqlCommand("insert into ilaclar (ilac_ad, ilac_ucret, ilac_resim) values (@p1,@p2,@p3)", connection);
            command.Parameters.AddWithValue("@p1", textBox1.Text);
            command.Parameters.AddWithValue("@p2", textBox2.Text);
            command.Parameters.AddWithValue("@p3", binaryData);
            command.ExecuteNonQuery();
            
            connection.Close();
            MessageBox.Show("Successfully added.");
        }

        
    }
}
