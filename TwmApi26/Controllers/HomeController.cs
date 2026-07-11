using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using Microsoft.Data.SqlClient;
using TwmApi26.Models;
using System.Net.Http;

namespace TwmApi26.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            List<Student> lst = new List<Student>();

            string cmd = "SELECT * FROM Student";
            DataTable dt = DatabaseManager.DisplayRecords(cmd);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Student std = new Student
                    {
                        Name = row["name"].ToString(),
                        Email = row["email"].ToString(),
                        Mobile = row["mobile"].ToString(),
                        Message = row["message"].ToString()
                    };

                    lst.Add(std);
                }
            }

            return Ok(lst);
        }
        // POST api/values

        [HttpPost]
        public string InsertContact(Student std)
        {
            string res = "";
            string cmd = "insert into student values('" + std.Name + "','" + std.Email + "','" + std.Mobile + "','" + std.Message + "')";
            if (DatabaseManager.Insert_update_delete(cmd))
                res = "Data Inserted";
            else
                res = "not inserted";
            return res;
        }

        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            string res = "";

            string cmd = $"DELETE FROM Student WHERE Email = '{email}'";

            if (DatabaseManager.Insert_update_delete(cmd))
                res = "Data deleted successfully.";
            else
                res = "Data not deleted.";

            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update(Student std)
        {
            string res = "";

            string cmd = $"UPDATE Student SET " +
                         $"Name='{std.Name}', " +
                         $"Mobile='{std.Mobile}', " +
                         $"Message='{std.Message}' " +
                         $"WHERE Email='{std.Email}'";

            if (DatabaseManager.Insert_update_delete(cmd))
                res = "Data updated successfully.";
            else
                res = "Data not updated.";

            return Ok(res);
        }

    }
}
