using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Controller for Enemy APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EnemyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public EnemyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get details of all enemies
        /// </summary>
        /// <returns>List of All Enemies</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.enemies";
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
        /// Get Enemy details By Enemy ID
        /// </summary>
        /// <param name="id">Id of the enemy</param>
        /// <returns>Details of the enemy</returns>
        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            string query = @"select * from dbo.enemies where enemy_id=@id";
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
        /// Add Enemy Details
        /// </summary>
        /// <param name="enemy">Enemy object with all details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(Enemy enemy)
        {
            string query = @"
                            insert into dbo.enemies
                            ([max_health],[enemy_speed],[enemy_type],[enemy_level])
                            values (@MaxHealth,@EnemySpeed,@EnemyType,@EnemyLevel)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@MaxHealth", enemy.MaxHealth);
                myCommand.Parameters.AddWithValue("@EnemySpeed", enemy.EnemySpeed);
                myCommand.Parameters.AddWithValue("@EnemyType", enemy.EnemyType);
                myCommand.Parameters.AddWithValue("@EnemyLevel", enemy.EnemyLevel);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("Enemy Added Successfully");
        }

        /// <summary>
        /// Delete Enemy
        /// </summary>
        /// <param name="id">Id of the enemy</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.enemies
                             where enemy_id=@Id";
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

            return new JsonResult("Enemy Deleted Successfully");
        }
    }
}
