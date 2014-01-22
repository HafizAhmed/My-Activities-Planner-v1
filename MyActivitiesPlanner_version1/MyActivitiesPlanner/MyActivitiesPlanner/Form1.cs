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

namespace MyActivitiesPlanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
            string connectionString = @"Data Source=NEXIUS-01806;Initial Catalog=ActivitiesDB;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(connectionString);
            string commandString = "INSERT INTO [ActivitiesDB].[dbo].[Activities] (Date,StartTime,EndTime,Activity) VALUES ( @Date,@STime, @ETime, @Activity)";
              SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);   
              sqlCmd.Parameters.AddWithValue("@Date",dateTimePicker1.Value);
              sqlCmd.Parameters.AddWithValue("@STime", STimeTB.Text);
              sqlCmd.Parameters.AddWithValue("@ETime", ETimeTB.Text);
              sqlCmd.Parameters.AddWithValue("@Activity",ActTB.Text);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            MessageBox.Show("Activity has been added successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt= new DataTable();
            try
            {
                string connectionString = @"Data Source=NEXIUS-01806;Initial Catalog=ActivitiesDB;Integrated Security=True";
                SqlConnection sqlCon = new SqlConnection(connectionString);
                string commandString = "SELECT ActID 'ID', Date, StartTime 'Start Time', EndTime 'End Time', Activity, CompletionRate 'Completion Rate (%)', Completed 'Is Completed' FROM [ActivitiesDB].[dbo].[Activities] WHERE (Date >= @dateTimePicker2 AND date <= @dateTimePicker3)";
                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.Parameters.AddWithValue("@dateTimePicker2", dateTimePicker2.Value.AddHours(-12));
                sqlCmd.Parameters.AddWithValue("@dateTimePicker3", dateTimePicker3.Value);

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlCon.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                    string connectionString = @"Data Source=NEXIUS-01806;Initial Catalog=ActivitiesDB;Integrated Security=True";
                    SqlConnection sqlCon = new SqlConnection(connectionString);
                    string commandString = "UPDATE [ActivitiesDB].[dbo].[Activities] SET CompletionRate= @compRate, Completed=@completed WHERE ActID= @id";
                    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                    SqlParameter compRate = new SqlParameter("@compRate",SqlDbType.Float);
                    SqlParameter completed = new SqlParameter("@completed", SqlDbType.Bit);
                    SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                    sqlCmd.Parameters.Add(compRate);
                    sqlCmd.Parameters.Add(completed);
                    sqlCmd.Parameters.Add(id);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            //sqlCmd.Parameters.AddWithValue("@compRate", dataGridView1.CurrentRow.Cells["CompletionRate"].Value);
                            //sqlCmd.Parameters.AddWithValue("@completed", dataGridView1.CurrentRow.Cells["Completed"].Value);
                            //sqlCmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["ActID"].Value);
                            compRate.Value= row.Cells["Completion Rate (%)"].Value;
                            completed.Value = row.Cells["Is Completed"].Value;
                            id.Value = row.Cells["ID"].Value;

                            sqlCon.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCon.Close();
                        }
                    }
                    
                    MessageBox.Show("Activities has been updated successfully");
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
