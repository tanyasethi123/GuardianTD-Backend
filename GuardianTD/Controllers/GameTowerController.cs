using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Game Tower API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameTowerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public GameTowerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get All Game Tower Details
        /// </summary>
        /// <returns>Returns All Game Tower Details</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select ugt.*,t.tower_name from dbo.user_game_tower ugt
                    left join towers t on ugt.tower_id=t.tower_id";
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
        /// Get User Game Tower Details By Id
        /// </summary>
        /// <param name="id">Id of the User Game Tower</param>
        /// <returns>Returns the User Game Tower Details By Id</returns>
        [HttpGet("UserGame/{id}")]
        public JsonResult GetByUserGameId(int id)
        {
            string query = @"select ugt.*,t.tower_name from dbo.user_game_tower ugt
                    left join towers t on ugt.tower_id=t.tower_id where ugt.user_game_id=@UserGameId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserGameId", id);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Add User Game Tower Details
        /// </summary>
        /// <param name="userGameTower">User Game Tower Object with details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(UserGameTower userGameTower)
        {
            string query = @"
                            insert into dbo.user_game_tower
                            ([user_game_id],[tower_id],[tower_position_x],[tower_position_y],[tower_position_z])
                            values (@UserGameId,@TowerId,@TowerPositionX,@TowerPositionY,@TowerPositionZ)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserGameId", userGameTower.UserGameId);
                myCommand.Parameters.AddWithValue("@TowerId", userGameTower.TowerId);
                myCommand.Parameters.AddWithValue("@TowerPositionX", userGameTower.TowerPositionX);
                myCommand.Parameters.AddWithValue("@TowerPositionY", userGameTower.TowerPositionY);
                myCommand.Parameters.AddWithValue("@TowerPositionZ", userGameTower.TowerPositionZ);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Game Tower Added Successfully");
        }

        /// <summary>
        /// Delete User Game Tower By Id
        /// </summary>
        /// <param name="id">Id of the User Game Tower</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.user_game_tower
                             where user_game_tower_id=@Id";
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

            return new JsonResult("User Game Tower Deleted Successfully");
        }
    }
}
