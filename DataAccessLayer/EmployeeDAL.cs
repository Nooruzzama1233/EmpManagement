using EmpManagement.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmpManagement.DataAccessLayer
{
    public class EmployeeDAL
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["EmpConnectionString"].ToString();
        public EmployeeDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["EmpConnectionString"].ToString();
        }

        public DataTable getAllEmployee(int PageSize, int PageNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        DataTable dt = null;
                        sqlCmd.Connection = connection;
                        sqlCmd.CommandText = @"uspGetEmployeeDetails";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("PageSize", SqlDbType.VarChar).Value = PageSize;
                        sqlCmd.Parameters.Add("PageNumber", SqlDbType.VarChar).Value = PageNumber;

                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public DataTable getEmployeeById(int EmployeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        DataTable dt = null;
                        sqlCmd.Connection = connection;
                        sqlCmd.CommandText = @"uspGetEmployeeDetailsById";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("EmployeeId", SqlDbType.VarChar).Value = EmployeeId;

                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int SaveEmployee(EmployeeListDTO employeeListDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = connection;
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.CommandText = @"dbo.uspSaveEmployeeDetails";
                        sqlCmd.Parameters.Add("EmployeeName", SqlDbType.VarChar).Value = employeeListDTO.EmployeeName;
                        sqlCmd.Parameters.Add("EmployeeEmail", SqlDbType.VarChar).Value = employeeListDTO.EmployeeEmail;
                        sqlCmd.Parameters.Add("EmployeeAddress", SqlDbType.VarChar).Value = employeeListDTO.EmployeeAddress;
                        sqlCmd.Parameters.Add("EmployeePhone", SqlDbType.VarChar).Value = employeeListDTO.EmployeePhone;

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
                return 1;
            }
            return 0;
        }

        public int UpdateEmployee(EmployeeListDTO employeeListDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = connection;
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.CommandText = @"dbo.uspUpdateEmployeeDetails";
                        sqlCmd.Parameters.Add("EmployeeId", SqlDbType.BigInt).Value = employeeListDTO.EmployeeId;
                        sqlCmd.Parameters.Add("EmployeeName", SqlDbType.VarChar).Value = employeeListDTO.EmployeeName;
                        sqlCmd.Parameters.Add("EmployeeEmail", SqlDbType.VarChar).Value = employeeListDTO.EmployeeEmail;
                        sqlCmd.Parameters.Add("EmployeeAddress", SqlDbType.VarChar).Value = employeeListDTO.EmployeeAddress;
                        sqlCmd.Parameters.Add("EmployeePhone", SqlDbType.VarChar).Value = employeeListDTO.EmployeePhone;

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
                return 1;
            }
            return 0;
        }

        public int DeleteEmployeeById(int EmployeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = connection;
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.CommandText = @"dbo.uspDeleteEmployeebyId";
                        sqlCmd.Parameters.Add("EmployeeId", SqlDbType.BigInt).Value = EmployeeId;

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
    }
}