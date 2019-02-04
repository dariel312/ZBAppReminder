using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppManager.DB;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Web.Controllers
{
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly AppManagerContext appDB;

        public AppointmentController(AppManagerContext appDB)
        {
            this.appDB = appDB;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get(DateTime Start, DateTime End, int? EmployeeID)
        {
            if(!Request.Headers.Keys.Contains("Authorization"))
            {
                return Unauthorized();
            }

            var apps = appDB.Transactions
                .Select(m => new {
                    m.StartTime,
                    m.EndTime,
                    m.EmpId,
                    m.CustId,
                    m.Note,
                    m.Customer.FirstName,
                    m.Customer.LastName,
                    m.Customer.Phone
                })
                .Where(m => m.StartTime >= Start && m.EndTime <= End && 
                        (m.EmpId == null || m.EmpId == EmployeeID));
            return Ok(apps);
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("employee")]
        public async Task<IEnumerable<object>> GetEmployees()
        {
            var apps = await appDB.Employees
                .Select(m => new
                {
                    m.EmpName,
                    m.EmpId,
                    m.Position
                })
                .ToListAsync();
            return apps;
        }

    }
}
