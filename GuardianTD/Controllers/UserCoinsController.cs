using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Controller for User Coins APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoinsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public UserCoinsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get All User Coins
        /// </summary>
        /// <returns>Returns all User Coins Details</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ut.*,u.user_name from dbo.user_coins ut
                left join users u on ut.user_id=u.user_id";
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
        /// Get User's Coins by User Id
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>Returns details of the User's Coins by User Id</returns>
        [HttpGet("User/{id}")]
        public JsonResult GetByUserId(int id)
        {
            string query = @"select uc.*,u.user_name from dbo.user_coins uc
                left join users u on uc.user_id=u.user_id where uc.user_id=@UserId";
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
        /// Get User's Coins by Coin Type
        /// </summary>
        /// <param name="cType">Id of the User</param>
        /// <returns>Returns details of the User's Coins by Coin Type</returns>
        [HttpGet("User/{coinType}")]
        public JsonResult GetByCoinType(string coinType)
        {
            string query = @"select uc.*,u.user_name from dbo.user_coins uc
                left join users u on uc.user_id=u.user_id where uc.coin_type=@CoinType";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@CoinType", coinType);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Add Coins for a User
        /// </summary>
        /// <param name="userCoins">Object with User's Coins Details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(UserCoins userCoins)
        {
            string query = @" insert into dbo.user_coins ([user_id], [coins],[coin_type])
                            values (@UserId, @NumberOfCoins, @CoinType)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserId", userCoins.UserId);
                myCommand.Parameters.AddWithValue("@CoinType", userCoins.CoinType);
                myCommand.Parameters.AddWithValue("@NumberOfCoins", userCoins.NumberOfCoins);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Coins Added Successfully");
        }

        /// <summary>
        /// Delete a User's Coins
        /// </summary>
        /// <param name="id">Id of the User Coins</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.user_coins
                             where user_coins_id=@Id";
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

            return new JsonResult("User Coins Deleted Successfully");
        }
    }
}
