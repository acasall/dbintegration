using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace dbintegration
{
    public partial class MainForm : Form
    {
        string connstring = "Server =localhost; Database= studentdb;User= root; Password= iHateminions9!";
        MySqlConnection connection = null; //null indicates no value, global connection with no value
        public MainForm()
        {
            InitializeComponent();
        }

        public void OpenDbConnection() //method
        {
            try
            {
                //creates/establishes a new database connection
                connection = new MySqlConnection(connstring);
                //opens the database connection when the connection is established so we can now communiate with it
                connection.Open();
            }
            catch (Exception ex) //exception object of the actual error message
            {
                MessageBox.Show("There was an error is establishing the connection: " + ex.Message);
            }
        }

        public void CloseDbConnection()
        {
            if (connection != null) 
            {
                connection.Close();
            } 
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            OpenDbConnection();

            string studentName= txtStudentName.Text;
            int studentID, studentAge;
            int.TryParse(txtStudentID.Text, out studentID);
            int.TryParse(txtStudentAge.Text, out studentAge);

            string sqlStatement = "Insert into student2 (studentid, studentname, studentage) values("+ studentID +", '"+ studentName +"', "+ studentAge +")";
            MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, connection); //new statement that executes the command
            sqlCommand.ExecuteNonQuery(); //executes statement against the studentdb
            MessageBox.Show("Added data to the Student2 table"); 

            CloseDbConnection();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            OpenDbConnection();

            //creates the sql statement to be executed as a string
            string sqlStatement = "Select * from student2";
            //create a MySqlCommand object. Parameters are the sql statement and the db connection object 
            MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, connection);
            //Creates a MySqlReader objcted to read data from student
            MySqlDataReader reader = sqlCommand.ExecuteReader(); //reads all the data from student2
            //Creates a DataTable object to store data as rows and columns in memory
            DataTable dt = new DataTable(); //stores the data 
            dt.Load(reader); 
            //display data in grdData
            grdData.DataSource = dt;

            CloseDbConnection();
        }
    }

    
}
