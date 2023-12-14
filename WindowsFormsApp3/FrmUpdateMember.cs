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

namespace WindowsFormsApp3
{
    public partial class FrmUpdateMember : Form
    {
        private int Age, Count;
        private long studentID;
        private string FirstName, MiddleName, LastName, Gender, Program;
        SqlConnection con;

        String sqlCon;

        private void button1_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE ClubMembers SET FirstName = @firstName, MiddleName = @middleName, LastName = @lastName, Age = @age, Gender = @gender, Program = @program WHERE StudentID = @id";

            con.ConnectionString = sqlCon;
            using (con)
            {

                if (con.State != ConnectionState.Open)
                    con.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", studentID);
                    cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@middleName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@lastName", textBox3.Text);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(textBox4.Text));
                    cmd.Parameters.AddWithValue("@gender", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@program", comboBox3.Text);

                    cmd.ExecuteNonQuery();
                }
                
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            String query = "SELECT * FROM ClubMembers WHERE StudentID = @id";
            con.ConnectionString = sqlCon;
            using (con)
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", comboBox1.Text);


                    SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                studentID = dr.GetInt64(1);
                FirstName = dr.GetString(2);
                MiddleName = dr.GetString(3);
                LastName = dr.GetString(4);
                Age = dr.GetInt32(5);
                Gender = dr.GetString(6);
                Program = dr.GetString(7);

                fillData();
                dr.Close();
            }
        }
        con.Close();
            }
}

        void fillData()
        {
            comboBox1.Text = studentID.ToString();
            textBox1.Text = FirstName;
            textBox3.Text = LastName;
            textBox2.Text = MiddleName;
            comboBox3.Text = Program;
            textBox4.Text = Age.ToString();
            comboBox2.Text = Gender;
        }

        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            sqlCon = $@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename={System.Environment.CurrentDirectory}\Database1.mdf;
            Initial Catalog = Database1;
            Integrated Security=True";
            con = new SqlConnection(sqlCon);

            String query = "SELECT * FROM ClubMembers";

            con.ConnectionString = sqlCon;
            using (con)
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", comboBox1.Text);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr.GetInt64(1));
                    }
                    dr.Close();
                }
                con.Close();
            }
        }
    }
}
