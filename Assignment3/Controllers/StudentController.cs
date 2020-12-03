using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;
using Microsoft.Ajax.Utilities;

namespace Assignment3.Controllers
{
    public class StudentController : Controller
    {
      
        //GET Student/StudentList
        public ActionResult StudentList()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents();
            return View(Students);
        }

        //GET Student/ShowStudent/{id};
        public ActionResult ShowStudent(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);

        }

        // GET Student/StudentAdd
        public ActionResult StudentAdd()
        {
            return View();
        }
        //POST : /Student/StudentCreate
        [HttpPost]
        public ActionResult StudentCreate(string StudentFname, string StudentLname, string StudentNumber)

        {
            //Identify that this method is running
            //Identify the inputs provided from the form




            Student NewStudent = new Student();
            NewStudent.StudentFname = StudentFname;
            NewStudent.StudentLname = StudentLname;
            NewStudent.StudentNumber = StudentNumber;
          

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);

            return RedirectToAction("StudentList");
        }
        //POST: / Student/StudentDelete/{id}
        public ActionResult StudentDelete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);
            return RedirectToAction("StudentList");
        }

        //POST / Student/StudentDeleteConfirm{id}
        public ActionResult StudentDeleteConfirm(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }
    }
}