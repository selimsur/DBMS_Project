using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace PharmacyManagement
{
    public partial class Form2 : Form
    {
        MySqlConnection connection;
        MySqlCommand command;
        string connectionString = "Server=localhost;Database=pharmacymanagementsystem;user=root;Pwd=;SslMode=none";
        byte[] binaryData;
        byte[] defaultBinaryData = File.ReadAllBytes("C:\\Users\\asus\\Desktop\\databaseProject\\DBMS_Project\\noImage.jpg");
        string ilac_ad, ilac_ucret;

        public Form2()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            userLoad();
            medicineLoad();
            //downloadExcelFile();
            excelParser();
            medicineStore();
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

        private void medicineStore()
        {
            connection.Open();
            command = new MySqlCommand("SELECT * FROM ilac_depo", connection);
            command.CommandType = CommandType.Text;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows != null)
            {
                connection.Open();
                command = new MySqlCommand("insert into ilaclar (ilac_ad, ilac_ucret, ilac_resim) values (@p10,@p11,@p12)", connection);
                command.Parameters.AddWithValue("@p10", ilac_ad);
                command.Parameters.AddWithValue("@p11", ilac_ucret);
                command.Parameters.AddWithValue("@p12", defaultBinaryData);
                command.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("Successfully added.");

            }

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                ilac_ad = row.Cells[2].Value.ToString();
                ilac_ucret = row.Cells[3].Value.ToString();
            }
        }

        private void excelParser()
        {
            string filePath;
            string fileName;

            filePath = "C:\\Users\\asus\\Desktop\\databaseProject\\DBMS_Project\\ilaclar.xlsx";
            fileName = "ilaclar.xlsx";

            ExcelApp.Application excelApp = new ExcelApp.Application();
            if (excelApp == null){ 
                MessageBox.Show("Please install Excel.");
                return;
            }

            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(filePath);
            ExcelApp._Worksheet excelSheet = excelBook.Sheets[1];
            ExcelApp.Range excelRange = excelSheet.UsedRange;
            int satirSayisi = excelRange.Rows.Count;
            int sutunSayisi = excelRange.Columns.Count;
          
            connection.Open();

            for(int i = 2; i < excelRange.Rows.Count; i++){
                command = new MySqlCommand("insert into ilac_depo (ilac_barkot, ilac_ad, ilac_ucret, ilac_firma) values (@p5,@p6,@p7,@p8)", connection);

                command.Parameters.AddWithValue("@p5", excelRange.Cells[i, 1].Value2.ToString());
                command.Parameters.AddWithValue("@p6", excelRange.Cells[i, 2].Value2.ToString());
                command.Parameters.AddWithValue("@p7", excelRange.Cells[i, 3].Value2.ToString());
                command.Parameters.AddWithValue("@p8", excelRange.Cells[i, 4].Value2.ToString());

                command.ExecuteNonQuery();
            }


            connection.Close();

            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

        }

        private void downloadExcelFile()
        {

            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://selcukecza.com.tr/dosyalar/ilacfiyatvebarkod/02-11-2022-ilac-fiyat.xlsx", "C:\\Users\\asus\\Desktop\\databaseProject\\DBMS_Project\\ilaclar.xlsx");
            webClient.Dispose();

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            userLoad();
            medicineLoad();
            medicineStore();
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
