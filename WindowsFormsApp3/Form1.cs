using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentID;

        private void button2_Click(object sender, EventArgs e)
        {
            FrmUpdateMember frmUpdateMember = new FrmUpdateMember();
            frmUpdateMember.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshListofClubMembers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentID = Convert.ToInt32(textBox1.Text);
            FirstName = textBox2.Text;
            MiddleName = textBox4.Text;
            LastName = textBox3.Text;
            Age = Convert.ToInt16(textBox5.Text);
            Gender = comboBox2.Text;
            Program = comboBox1.Text;

            clubRegistrationQuery.RegisterStudent(RegistrationID(), StudentID, FirstName,
                MiddleName, LastName,
                Age, Gender, Program);
            RefreshListofClubMembers();

        }

        private long SudentId;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListofClubMembers();
        }

        void RefreshListofClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;

        }

        int RegistrationID()
        {
            count++;
            return count;
        }

    }
}
