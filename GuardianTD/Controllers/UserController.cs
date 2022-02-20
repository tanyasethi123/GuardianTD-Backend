using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using GuardianTD.Models;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// APIs for User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get All User Details
        /// </summary>
        /// <returns>Returns details of all Users</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.users";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>Returns the User Details By Id</returns>
        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            string query = @"select * from dbo.users where user_id=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@id", id);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user">User object with details</param>
        /// <returns>Returns the id of the new User created</returns>
        [HttpPost]
        public JsonResult Post(User user)
        {
                string query = @"
                            insert into dbo.users
                            ([first_name],[last_name],[created_at],[email],[password],[active]) output INSERTED.user_id
                            values (@FirstName,@LastName,@CreatedAt,@Email,@Password,@Active);SELECT SCOPE_IDENTITY()";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            JObject returnObj = new JObject();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                myCommand.Parameters.AddWithValue("@LastName", user.LastName);
                myCommand.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                myCommand.Parameters.AddWithValue("@Email", user.Email);
                myCommand.Parameters.AddWithValue("@Password", user.Password);
                myCommand.Parameters.AddWithValue("@Active", 1);
                int userId = (int)myCommand.ExecuteScalar();
                myCon.Close();
                returnObj.Add("userid", userId);
                returnObj.Add("message", "User Added Successfully");
            }
            return new JsonResult(returnObj);
        }

        /// <summary>
        /// Update User Details
        /// </summary>
        /// <param name="user">User object with details</param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"
                            update dbo.users
                            set first_name= @FirstName,last_name=@LastName,email=@Email,password=@Password where user_id=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", user.Id);
                myCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                myCommand.Parameters.AddWithValue("@LastName", user.LastName);
                myCommand.Parameters.AddWithValue("@Email", user.Email);
                myCommand.Parameters.AddWithValue("@Password", user.Password);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Details Updated Successfully");
        }

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.users
                             where user_id=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", id);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Deleted Successfully");
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="emailId">Emailid of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>Returns if the User Credentials are valid or invalid</returns>
        [HttpGet("Authenticate")]
        public ActionResult Authenticate(string emailId,string password)
        {
            bool hasResult = false;
            string query = @"select * from dbo.users where email=@EmailId and password=@Password";
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Emailid", emailId);
                myCommand.Parameters.AddWithValue("@Password", password);
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    hasResult = true;
                }
                myReader.Close();
                myCon.Close();
            }
            if(hasResult)
                return new OkResult();
            else
                return BadRequest("Invalid UserId or Password.Please try again.");

        }
    }
}
