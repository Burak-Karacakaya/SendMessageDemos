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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjeDemos;Integrated Security=True");

        public string number;

        void inbox()
        {
            SqlDataAdapter dataAdapterInbox = new SqlDataAdapter
                ("select Messages.Id, (name + ' ' + surname) as [from], subject, body from Messages inner join Persons on Messages.[From] = Persons.Number where [To]="
                + number, connection);
            DataTable dataTableInbox = new DataTable();
            dataAdapterInbox.Fill(dataTableInbox);
            dataGridView1.DataSource = dataTableInbox;
        }

        void outbox()
        {
            SqlDataAdapter dataAdapterOutbox = new SqlDataAdapter
                ("select Messages.Id, (name + ' ' + surname) as [To], subject, body from Messages inner join Persons on Messages.[to] = Persons.Number where [From]="
                + number, connection);
            DataTable dataTableOutbox = new DataTable();
            dataAdapterOutbox.Fill(dataTableOutbox);
            dataGridView2.DataSource = dataTableOutbox;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lblNumber.Text = number;
            inbox();
            outbox();

            //Name and surname Get

            connection.Open();
            SqlCommand command = new SqlCommand("select name, surname from Persons  where number=" +number, connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                lblNameSurnameTextbox.Text = sqlDataReader[0] + " " + sqlDataReader[1];
            }
            connection.Close();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Messages ([From],[To],subject,body) values (@p1,@p2,@p3,@p4)", connection);
            command.Parameters.AddWithValue("@p1", number);
            command.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            command.Parameters.AddWithValue("@p3", textBox1.Text);
            command.Parameters.AddWithValue("@p4", richTextBox1.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Your message has been delivered.");
            outbox();

        }
    }
}
