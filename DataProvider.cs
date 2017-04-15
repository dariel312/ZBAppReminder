using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder
{
    static class DataProvider
    {
        public static string ConnectionString
        {
            get
            {
                return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Properties.Settings.Default.DataBasePath + ";";
            }
        }

        public static ICollection<Transaction> GetAppointments()
        {
            var apps = new List<Transaction>();
            var tomorrow = DateTime.Today.AddDays(1);
            var sqlCmd = "SELECT * from Transactions WHERE StartTime > #" + tomorrow.ToString("d") + " 12:00:00 AM# AND StartTime < #" + tomorrow.ToString("d") + " 11:59:00 PM#";

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
        public static ICollection<Customer> GetCustomers()
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
        public static ICollection<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            OleDbDataReader reader;
            using (executeQuery("SELECT * from Employees", out reader))
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

        private static OleDbConnection executeQuery(string query, out OleDbDataReader reader)
        {
            try
            {
                var connection = new OleDbConnection(ConnectionString);
                connection.Open();
                var cmd = new OleDbCommand(query, connection, connection.BeginTransaction());
                reader = cmd.ExecuteReader();
                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
