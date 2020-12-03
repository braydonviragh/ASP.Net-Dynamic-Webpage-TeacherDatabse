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
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Formal SQL query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%"+SearchKey+"%') or lower(teacherlname) like lower('%"+SearchKey+"%') or lower(concat(teacherfname, ' ', teacherlname)) like lower('%"+SearchKey+"%')";

            //Turn query result into variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create empty list of Teacher names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through the result set rows
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;

                //Add Teacher to list
                Teachers.Add(NewTeacher);
            }

            //closing connection 
            Conn.Close();

            return Teachers;
        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Formal SQL query
            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            //Turn query result into variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.Salary = Salary;
            }

            return NewTeacher;
        }

        [HttpPost]
        public void AddTeacher (Teacher NewTeacher)
        {
            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Formal SQL query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                "values (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";

            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        [HttpPost]
        public void DeleteTeacher (int id)
        {
            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Formal SQL query
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
    }
}
