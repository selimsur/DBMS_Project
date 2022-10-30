using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        public Form2()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            command = new MySqlCommand("SELECT * FROM users",connection);
            command.CommandType = CommandType.Text;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
          
        }
    }
}
