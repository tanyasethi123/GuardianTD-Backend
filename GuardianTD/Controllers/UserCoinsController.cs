using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
        /// Get All User Coins of all Users
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
            string query = @"select uc.coin_type,uc.coins from dbo.user_coins uc
                where uc.user_id=@UserId group by uc.coin_type,uc.coins";
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
        /// <param name="userId">Id of the User</param>
        /// <param name="coinType">Coin type</param>
        /// <returns>Returns details of the User's Coins by Coin Type</returns>
        [HttpGet("UserId/{userId}/CoinType/{coinType}")]
        public JsonResult GetByUserIdAndCoinType(int userId,string coinType)
        {
            string query = @"select uc.*,u.user_name from dbo.user_coins uc
                left join users u on uc.user_id=u.user_id where uc.coin_type=@CoinType and uc.user_id=@UserId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@CoinType", coinType);
                myCommand.Parameters.AddWithValue("@UserId", userId);
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
            string query = @" insert into dbo.user_coins ([user_id], [coins],[coin_type],[created_at],[updated_at])
                            values (@UserId, @NumberOfCoins, @CoinType,@CreatedAt,@CreatedAt)";
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
                myCommand.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Coins Added Successfully");
        }

        /// <summary>
        /// Update User Coins
        /// </summary>
        /// <param name="userCoins">Object with User's Coins Details</param>
        /// <returns></returns>
        [HttpPatch]
        public JsonResult Patch(UserCoins userCoins)
        {
            string query = @"
                            update dbo.user_coins
                            set coins= @Coins,updated_at=@UpdatedAt where user_id=@Id and coin_type=@CoinType";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", userCoins.UserId);
                myCommand.Parameters.AddWithValue("@Coins", userCoins.NumberOfCoins);
                myCommand.Parameters.AddWithValue("@CoinType", userCoins.CoinType);
                myCommand.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Coins Updated Successfully");
        }

        /// <summary>
        /// Delete a User's Coins for a particular coin type
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <param name="coinType">Type of the Coin</param>
        /// <returns></returns>
        [HttpDelete("UserId/{id}/CoinType/{coinType}")]
        public JsonResult Delete(int id,string coinType)
        {
            string query = @"
                            delete from dbo.user_coins
                             where user_id=@Id and coin_type=@CoinType";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", id);
                myCommand.Parameters.AddWithValue("@CoinType", coinType);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Coins for User Deleted Successfully");
        }
    }
}
