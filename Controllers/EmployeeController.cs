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
            return View();
        }

        public JsonResult GetData(int PageSize=5, int PageNumber=1)
        {
            List<EmployeeListDTO> employees = objEmployeeBAL.getAllEmployee(PageSize, PageNumber);
            return Json(employees, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult AddNewEmployee(EmployeeListDTO objEmployeeListDTO)
        {           
            if (objEmployeeListDTO != null)
            {
                int rows = objEmployeeBAL.SaveEmployee(objEmployeeListDTO);
                if(rows==0)
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
            if (EmployeeId > 0 )
            {
                EmployeeListDTO employeesById= objEmployeeBAL.getEmployeeById(EmployeeId);
                if (employeesById !=null)
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
           int r = objEmployeeBAL.DeleteEmployee(EmployeeId);
            if (r == 0)
                return Json("Employee Deleted Successfully.", JsonRequestBehavior.AllowGet);
            else
                return Json("Error Occured.", JsonRequestBehavior.AllowGet);            
        }
    }
}