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

    }
}