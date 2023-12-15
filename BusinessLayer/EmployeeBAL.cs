using EmpManagement.DataAccessLayer;
using EmpManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace EmpManagement.BusinessLayer
{
    public class EmployeeBAL
    {
        private EmployeeDAL objEmployeeDAL = null;

        public EmployeeBAL()
        {
            objEmployeeDAL = new EmployeeDAL();
        }
        public List<EmployeeListDTO> getAllEmployee(int PageSize, int PageNumber)
        {
            List<EmployeeListDTO> employees = null;
            try
            {
                DataTable employeeDataTable = objEmployeeDAL.getAllEmployee(PageSize, PageNumber);
                employees = new List<EmployeeListDTO>();

                if(employeeDataTable.Rows.Count >0 && employeeDataTable !=null)
                {
                    foreach (DataRow row in employeeDataTable.Rows)
                    {
                        EmployeeListDTO employee = new EmployeeListDTO
                        {
                            EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                            EmployeeName = row["EmployeeName"].ToString(),
                            EmployeeEmail = row["EmployeeEmail"].ToString(),
                            EmployeeAddress = row["EmployeeAddress"].ToString(),
                            EmployeePhone = row["EmployeePhone"].ToString(),
                            RowNumber = Convert.ToInt32(row["RowNumber"]),
                            TotalRecord = Convert.ToInt32(row["TotalRecord"]),
                        };
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return employees;
        }

        public EmployeeListDTO getEmployeeById(int EmployeeId)
        {
            EmployeeListDTO employees = null;
            try
            {
                DataTable employeeDataTable = objEmployeeDAL.getEmployeeById(EmployeeId);
                employees = new EmployeeListDTO();

                if (employeeDataTable.Rows.Count > 0 && employeeDataTable != null)
                {
                    foreach (DataRow row in employeeDataTable.Rows)
                    {
                        employees=new EmployeeListDTO()
                        {
                            EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                            EmployeeName = row["EmployeeName"].ToString(),
                            EmployeeEmail = row["EmployeeEmail"].ToString(),
                            EmployeeAddress = row["EmployeeAddress"].ToString(),
                            EmployeePhone = row["EmployeePhone"].ToString(),
                            RowNumber = Convert.ToInt32(row["RowNumber"]),
                            TotalRecord = Convert.ToInt32(row["TotalRecord"]),

                        };
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return employees;
        }
        public int SaveEmployee(EmployeeListDTO listDTO)
        {
            return objEmployeeDAL.SaveEmployee(listDTO);            
        }

        public int UpdateEmployee(EmployeeListDTO  listDTO)
        {
            objEmployeeDAL.UpdateEmployee(listDTO);
            return 0;
        }

        public int DeleteEmployee(int EmployeeId)
        {
            objEmployeeDAL.DeleteEmployee(EmployeeId);
            return 0;
        }

    }
}