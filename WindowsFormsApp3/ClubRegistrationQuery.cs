using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        public DataTable dataTable;
        public BindingSource bindingSource;

        private string connectionString;

        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;


        public ClubRegistrationQuery()
        {
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename={System.Environment.CurrentDirectory}\Database1.mdf;
            Initial Catalog = Database1;
            Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            bool success = false;

            try
            {
                string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Program, Gender FROM ClubMembers";

                using (sqlConnect)
                {
                    sqlConnect.ConnectionString = connectionString;

                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();

                    sqlCommand = new SqlCommand(ViewClubMembers, sqlConnect);
                    sqlAdapter = new SqlDataAdapter(sqlCommand);

                    dataTable.Clear();
                    sqlAdapter.Fill(dataTable);
                    bindingSource.DataSource = dataTable;

                    success = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
                success = false;
            }
            finally
            {
                sqlConnect.Close();
            }

            return success;
        }
        public bool RegisterStudent(int ID, long StudentID,
          string FirstName, string MiddleName, string LastName,
          int Age, string Program, string Gender)
        {
            using (sqlCommand = new SqlCommand(@"INSERT INTO ClubMembers 
                (Id, StudentID, FirstName, MiddleName, LastName, Age, Gender, Program) 
                VALUES(@ID, @StudentID, @FirstName, @MiddleName, 
                @LastName, @Age, @Program, @Gender)", sqlConnect))
            {
                sqlConnect.ConnectionString = connectionString;
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
                sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
                sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
                sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;
                sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnect.Close();

                return true;
            }
        }
    }
}

