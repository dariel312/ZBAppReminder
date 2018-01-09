﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Core
{
    internal class DataProvider
    {
        string connectionString;


        internal DataProvider(string FilePath)
        {
            connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={ FilePath };";
        }



        public ICollection<Transaction> GetAppointments(DateTime StartTime, DateTime EndTime)
        {
            var apps = new List<Transaction>();
         
            var sqlCmd = "SELECT * from Transactions WHERE StartTime > #" + StartTime.ToString("d") + " 12:00:00 AM# AND StartTime < #" + EndTime.ToString("d") + " 11:59:00 PM#";

            OleDbDataReader reader = null;
            using (executeQuery(sqlCmd, out reader))
            {
                while (reader.Read())
                {
                    Transaction tran = new Transaction()
                    {
                        TransactionID = (int)reader["TranID"],
                        StartTime = (DateTime)reader["StartTime"],
                        EmployeeID = (int)reader["EmpID"],
                        CustomerID = (int)reader["CustID"]
                    };

                    apps.Add(tran);
                }
            }

            return apps;
        }
        public ICollection<Customer> GetCustomers()
        {
            var cust = new List<Customer>();
            OleDbDataReader reader;
            using (executeQuery("SELECT * from Customers", out reader))
            {
                while (reader.Read())
                    cust.Add(new Customer()
                    {
                        CustomerID = (int)reader["CustID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Telephone = (string)reader["Phone"]
                    });
            }
            return cust;
        }
        public ICollection<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (executeQuery("SELECT * from Employees", out var reader))
            {
                while (reader.Read())
                    employees.Add(new Employee()
                    {
                        EmployeeID = (int)reader["EmpID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        EmployeeName = (string)reader["EmpName"],

                    });
            }
            return employees;
        }

        private OleDbConnection executeQuery(string query, out OleDbDataReader reader)
        {
            try
            {
                var connection = new OleDbConnection(connectionString);
                connection.Open();

                reader = new OleDbCommand(query, connection, connection.BeginTransaction())
                    .ExecuteReader();
                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}