using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Hotel.Controllers
{
    public class SignUpController : Controller
    {
        public readonly IConfiguration _configuration;
        public SignUpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
         public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(RegisteredUser registeredUser)
        {
            if (ModelState.IsValid)
            {
                string connectString = _configuration.GetConnectionString("DBConnection");
               // string connectt = "Data Source=DECAGON\\SQLEXPRESS;Initial Catalog=HotelsiteDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO RegisteredUser (Id, FirstName, LastNAme, EmailAddress, Password, DateTime) " +
                                      "VALUES (@Id, @FirstName, @LastName, @EmailAddress, @Password, @DateTime)";
                    cmd.Connection = con;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@Id", registeredUser.Id);
                    cmd.Parameters.AddWithValue("@FirstName", registeredUser.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", registeredUser.LastName);
                    cmd.Parameters.AddWithValue("@EmailAddress", registeredUser.EmailAddress);
                    cmd.Parameters.AddWithValue("@Password", registeredUser.Password);
                    cmd.Parameters.AddWithValue("@DateTime", registeredUser.RegisteredDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("LogIn", "LogIn");
            }
            return View();
        }


         
    }
}
