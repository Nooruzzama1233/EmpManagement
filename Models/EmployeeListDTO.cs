using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpManagement.Models
{
    public class EmployeeListDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeePhone { get; set; }
        public int RowNumber { get; set; }
        public int TotalRecord { get; set; }


        public EmployeeListDTO()
        {
            EmployeeId = 0;
            EmployeeName    = string.Empty;
            EmployeeEmail   = string.Empty;
            EmployeeAddress = string.Empty;
            EmployeePhone   = string.Empty;
            RowNumber = 0;
            TotalRecord = 0;
        }

    }
}