using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// User Tower APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserTowerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public UserTowerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get All User Towers
        /// </summary>
        /// <returns>Returns all User Tower Details</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ut.*,u.first_name,u.last_name,t.* from dbo.user_towers ut
                left join users u on ut.user_id=u.user_id
                left join towers t on ut.tower_id=t.tower_id";
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
        /// Get User's Towers by User Id
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>Returns details of the User's Towers by User Id</returns>
        [HttpGet("User/{id}")]
        public JsonResult GetByUserId(int id)
        {
            string query = @"select ut.*,u.first_name,u.last_name,t.* from dbo.user_towers ut
                left join users u on ut.user_id=u.user_id
                left join towers t on ut.tower_id=t.tower_id where ut.user_id=@UserId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserId", id);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Add Tower for a User
        /// </summary>
        /// <param name="userTower">Object with User's Tower Details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(UserTower userTower)
        {
            string query = @"
                            insert into dbo.user_towers
                            ([user_id],[tower_id])
                            values (@UserId,@TowerId)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserId", userTower.UserId);
                myCommand.Parameters.AddWithValue("@TowerId", userTower.TowerId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Tower Added Successfully");
        }

        /// <summary>
        /// Delete a User's Tower
        /// </summary>
        /// <param name="id">Id of the User Tower</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.user_towers
                             where user_tower_id=@Id";
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

            return new JsonResult("User Tower Deleted Successfully");
        }
    }
}
