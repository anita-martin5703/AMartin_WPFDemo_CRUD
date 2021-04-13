// Anita Martin
// amartin98@cnm.edu
// Title: CRUD Demo

using System.Collections.Generic;
using System.Windows;

namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private Student st;

        public Student Student
        {
            get { return st; }
            set { st = value; }
        }

        public StudentWindow() : this(new Student())
        {
        }

        public StudentWindow(Student st)
        {
            InitializeComponent();
            this.st = st;

            //set drop downs
            School school = new School();
            cbxCourse.ItemsSource = school.Course.DefaultView;
            cbxMajor.ItemsSource = school.Major.DefaultView;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Instantiate a class object
            //Student st = new Student();

            //Set the values from our controls into the class
            st.FirstName = txbFirstName.Text;
            st.LastName = txbLastName.Text;
            st.StudentNumber = txbStudentNum.Text;
            st.Major = cbxMajor.SelectedValue.ToString();

            List<Assignment> scores = new List<Assignment>();
            foreach (Assignment score in lbScores.Items)
            {
                scores.Add(score);
            }
            st.Scores = scores;

            //Set the results from the class into a control on the form
            txbResults.Text = st.ToString();

            DialogResult = true;
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Assignment assign = new Assignment();
            assign.Title = cbxCourse.SelectedValue.ToString();
            assign.Score = int.Parse(txbScore.Text);
            lbScores.Items.Add(assign);
        }
    }
}
