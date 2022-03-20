using GuardianTD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Controller for the User Achievements APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserAchievementsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public UserAchievementsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Add User Achievements
        /// </summary>
        /// <param name="userAchievement">User Achievement object with details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(UserAchievement userAchievement)
        {
            string query = @"
                            insert into dbo.user_achievement
                            ([user_id],[achievement_id])
                            values (@UserId,@AchievementId)";
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            DataTable table = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                myCommand.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Achievement Added Successfully");
        }

        /// <summary>
        /// Get User's Achievement Details By User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of User's Achievements</returns>
        [HttpGet("User/{userId}")]
        public JsonResult GetById(int userId)
        {
            string query = @"select ua.achievement_id,a.name,'claimed' as claim_status from user_achievement ua left join achievement a
                            on ua.achievement_id=a.achievement_id where ua.user_id=@Id
                            union
                            select achievement_id,name,'notclaimed' as claim_status from achievement
                             where achievement_id not in (select achievement_id from user_achievement where user_id=@Id)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", userId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Get User's All Achievement Details By User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of User's All Achievements</returns>
        [HttpGet("All/User/{userId}")]
        public JsonResult GetAllById(int userId)
        {
            string query = @"select uc.coin_type as achievement,uc.coins as value from dbo.user_coins uc
                where uc.user_id=@Id group by uc.coin_type,uc.coins
                union
                select 'towersplaced' as achievement,count(*) as value from dbo.user_towers
                where user_id=@Id group by user_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", userId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }
    }
}
