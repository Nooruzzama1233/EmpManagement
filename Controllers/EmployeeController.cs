using EmpManagement.BusinessLayer;
using EmpManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpManagement.Controllers
{
    public class EmployeeController : Controller
    {

        private EmployeeBAL objEmployeeBAL = null;
        public EmployeeController()
        {
            objEmployeeBAL = new EmployeeBAL();
        }
        public ActionResult Index()
        {
            int pageSize = 5;
            int? page = 1;
            int pageNumber = (page ?? 1);
            List<EmployeeListDTO> employees = objEmployeeBAL.getAllEmployee(pageSize, pageNumber);
            var paginatedData = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)employees.Count / pageSize);

            return View();
        }

        public JsonResult GetData(int pageSize = 5, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            List<EmployeeListDTO> employees = objEmployeeBAL.getAllEmployee(pageSize, pageNumber);           

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNewEmployee(EmployeeListDTO objEmployeeListDTO)
        {
            if (objEmployeeListDTO != null)
            {
                int rows = objEmployeeBAL.SaveEmployee(objEmployeeListDTO);
                if (rows == 0)
                    return Json("Employee Added Successfully.", JsonRequestBehavior.AllowGet);
                else
                    return Json("Error Occured.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Please fill data.", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getEmployeeById(int EmployeeId)
        {
            if (EmployeeId > 0)
            {
                EmployeeListDTO employeesById = objEmployeeBAL.getEmployeeById(EmployeeId);
                if (employeesById != null)
                    return Json(employeesById, JsonRequestBehavior.AllowGet);
                else
                    return Json("Error Occured.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Please fill data", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateEmployee(EmployeeListDTO objEmployeeListDTO)
        {
            if (objEmployeeListDTO != null && objEmployeeListDTO.EmployeeId > 0)
            {
                int rows = objEmployeeBAL.UpdateEmployee(objEmployeeListDTO);
                if (rows == 0)
                    return Json("Employee Updated Successfully.", JsonRequestBehavior.AllowGet);
                else
                    return Json("Error Occured.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Please fill data", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteEmployee(int EmployeeId)
        {
            int r = 0;
            if (EmployeeId > 0)
            {
                r = objEmployeeBAL.DeleteEmployeeById(EmployeeId);
            }

            if (r == 0)
                return Json("Employee Deleted Successfully.", JsonRequestBehavior.AllowGet);
            else
                return Json("Error Occured.", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEmployees(string[] EmployeeIds)
        {
            int r = 0;
            int EmployeeId = 0;

            if (EmployeeIds.Length > 0 && EmployeeIds != null)
            {
                foreach (string id in EmployeeIds)
                {
                    int.TryParse(id, out EmployeeId);
                    r = objEmployeeBAL.DeleteEmployeeById(EmployeeId);
                }
            }

            if (r == 0)
                return Json("Selected Employees are Deleted Successfully.", JsonRequestBehavior.AllowGet);
            else
                return Json("Error Occured.", JsonRequestBehavior.AllowGet);
        }
    }
}