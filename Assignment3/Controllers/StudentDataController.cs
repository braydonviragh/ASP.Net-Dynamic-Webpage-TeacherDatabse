using Assignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {
            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Formal SQL query
            cmd.CommandText = "Select * from students";

            //Turn query result into variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create empty list of Student names
            List<Student> Students = new List<Student> { };

            //Loop through the result set rows
            while (ResultSet.Read())
            {
                uint StudentId = (uint)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];


                Student NewStudent = new Student();
                NewStudent.StudentId = (int)StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;

                //Add Student to list
                Students.Add(NewStudent);
            }

            //closing connection 
            Conn.Close();

            return Students;
        }

        internal Student FindStudent(uint id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Formal SQL query
            cmd.CommandText = "Select * from students where studentid = "+id;

            //Turn query result into variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                uint StudentId = (uint)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];


                NewStudent.StudentId = (int)StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
            }

            return NewStudent;
        }
    }
}