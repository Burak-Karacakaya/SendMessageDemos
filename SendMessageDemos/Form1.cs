using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SendMessageDemos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjeDemos;Integrated Security=True");


        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select * from Persons where number = @p1 and password = @p2", connection);
            command.Parameters.AddWithValue("@p1", mskdNumber.Text);
            command.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Form2 form2 = new Form2();
                form2.number = mskdNumber.Text;
                form2.Show();
            }
            else
            {
                MessageBox.Show("Wrong Info");
            }
            connection.Close();
        }
    }
}
