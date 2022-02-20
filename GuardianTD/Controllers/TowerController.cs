using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Tower Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TowerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public TowerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get All Tower Details
        /// </summary>
        /// <returns>Returns all Tower Details</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.towers";
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
        /// Get Tower Details By Id
        /// </summary>
        /// <param name="id">Id of the Tower</param>
        /// <returns>Returns the Tower Details By Id</returns>
        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            string query = @"select * from dbo.towers where tower_id=@id";
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
        /// Add Tower
        /// </summary>
        /// <param name="tower">Tower Object containing details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(Tower tower)
        {
            string query = @"
                            insert into dbo.towers
                            ([tower_range],[tower_type],[tower_name],[tower_damage],[tower_value])
                            values (@TowerRange,@TowerType,@TowerName,@TowerDamage,@TowerValue)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@TowerRange", tower.TowerRange);
                myCommand.Parameters.AddWithValue("@TowerType", tower.TowerType);
                myCommand.Parameters.AddWithValue("@TowerName", tower.TowerName);
                myCommand.Parameters.AddWithValue("@TowerDamage", tower.TowerDamage);
                myCommand.Parameters.AddWithValue("@TowerValue", tower.TowerValue);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("Tower Added Successfully");
        }

        /// <summary>
        /// Delete a Tower By Id
        /// </summary>
        /// <param name="id">Id of the Tower</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.towers
                             where tower_id=@Id";
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

            return new JsonResult("Tower Deleted Successfully");
        }

    }
}
