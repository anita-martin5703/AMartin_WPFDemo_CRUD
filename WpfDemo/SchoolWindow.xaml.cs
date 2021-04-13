// Anita Martin
// amartin98@cnm.edu
// Title: CRUD Demo

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for SchoolWindow.xaml
    /// </summary>
    public partial class SchoolWindow : Window
    {
        School school;
        public SchoolWindow()
        {
            InitializeComponent();
            school = new School();
            lbCampuses.DisplayMemberPath = "Name";
            lbCampuses.ItemsSource = school.Campus.DefaultView;
            lbCourses.DisplayMemberPath = "Title";
            lbCourses.ItemsSource = school.Course.DefaultView;
            lbMajor.DisplayMemberPath = "Title";
            lbMajor.ItemsSource = school.Major.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student();
            StudentWindow studentWindow = new StudentWindow(newStudent);

            //StudentWindow studentWindow = new StudentWindow();
            //Student newStudent = studentWindow.Student;
            studentWindow.ShowDialog();
            if (studentWindow.DialogResult == true)
            {
                lbStudents.Items.Add(newStudent);
            }


        }

        private void btnCampusAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            //If item is not selected then do an add operation
            if (lbCampuses.SelectedItem == null)
            {
                //Add item to database
                string insStr = "INSERT INTO Campus(Name) VALUES(@Name)";

                string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand insCmd = new SqlCommand(insStr, conn);
                    insCmd.Parameters.AddWithValue("Name", txbCampus.Text);
                    conn.Open();
                    insCmd.ExecuteNonQuery();
                }
            }
            else //Update selected item
            {
                string updStr = "UPDATE Campus SET Name = @Name WHERE CampusID = @CampusID";

                string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand updCmd = new SqlCommand(updStr, conn);
                    updCmd.Parameters.AddWithValue("Name", txbCampus.Text);
                    int campusID = (int)((DataRowView)lbCampuses.SelectedItem)["CampusID"];
                    updCmd.Parameters.AddWithValue("CampusID", campusID);
                    conn.Open();
                    updCmd.ExecuteNonQuery();
                }
            }

            //Refresh school and list box
            school.Refresh();
            lbCampuses.ItemsSource = school.Campus.DefaultView;
            lbCampuses.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbCampuses.SelectedItem != null)
            {
                string delStr = "DELETE FROM Campus WHERE CampusID = @CampusID";

                string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand delCmd = new SqlCommand(delStr, conn);
                    int campusID = (int)((DataRowView)lbCampuses.SelectedItem)["CampusID"];
                    delCmd.Parameters.AddWithValue("CampusID", campusID);
                    conn.Open();
                    delCmd.ExecuteNonQuery();
                }

                //Refresn school and list box
                school.Refresh();
                lbCampuses.ItemsSource = school.Campus.DefaultView;
                lbCampuses.Items.Refresh();
            }

        }
    }
}
