// Anita Martin
// amartin98@cnm.edu
// Title: CRUD Demo

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WPFDemo
{
    class School
    {
        private DataTable course;
        private DataTable major;
        private DataTable campus;
        private List<string> student;

        public DataTable Course
        {
            get { return course; }
            set { course = value; }
        }

        public DataTable Major
        {
            get { return major; }
            set { major = value; }
        }

        public DataTable Campus
        {
            get { return campus; }
            set { campus = value; }
        }

        public List<string> Student
        {
            get { return student; }
            set { student = value; }
        }

        public School()
        {
            Refresh();
        }

        public void Refresh()
        {
            campus = GetData("SELECT * FROM Campus");
            course = GetData("SELECT * FROM Course");
            major = GetData("SELECT * FROM Major");
        }

        private DataTable GetData(string sqlStr)
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlStr, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(selectCmd);
                da.FillSchema(dt, SchemaType.Source);
                da.Fill(dt);
            }
            return dt;
        }

    }

}
