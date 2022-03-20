using GuardianTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Controller for Game APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public GameController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get all Games
        /// </summary>
        /// <returns>List of all games</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.user_game";
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
        /// Get Game Details By User Id
        /// </summary>
        /// <param name="userId">Id of the User</param>
        /// <returns>Game Details by User Id</returns>
        [HttpGet("user/{userid}")]
        public JsonResult GetGameByUserId(int userId)
        {
            string query = @"select * from dbo.user_game where user_id=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@id", userId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Get Game Details By Game Id
        /// </summary>
        /// <param name="gameId">Id of the Game</param>
        /// <returns>Game Details by Game Id</returns>
        [HttpGet("{gameid}")]
        public JsonResult GetGameByGameId(int gameId)
        {
            string query = @"select * from dbo.user_game where user_game_id=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@id", gameId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Add Game Details
        /// </summary>
        /// <param name="userGame">Game Object with details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(UserGame userGame)
        {
            string query = @"
                            insert into dbo.user_game
                            ([user_id],[game_type],[user_score],[created_at],[updated_at],[enemy_id])
                            values (@UserId,@GameType,@UserScore,@CreatedAt,@UpdatedAt,@EnemyId)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserId", userGame.UserId);
                myCommand.Parameters.AddWithValue("@GameType", userGame.GameType);
                myCommand.Parameters.AddWithValue("@UserScore", userGame.UserScore);
                myCommand.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                myCommand.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                myCommand.Parameters.AddWithValue("@EnemyId", userGame.EnemyId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Game Mapping Added Successfully");
        }

        /// <summary>
        /// Update Game Details
        /// </summary>
        /// <param name="userGame">Game object with details to be updated</param>
        /// <returns></returns>
        [HttpPatch]
        public JsonResult Patch(UserGame userGame)
        {
            List<string> setConditions = new List<string>();
            if (userGame.UserScore != null)
                setConditions.Add(@"user_score = @UserScore ");
            if (userGame.EnemyId != null)
                setConditions.Add(@"enemy_id = @EnemyId ");

            string query = $"update dbo.user_game "+
            $"SET {string.Join(", ", setConditions)} " +
            $"where user_game_id=@Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Id", userGame.UserGameId);
                if (userGame.UserScore != null)
                    myCommand.Parameters.AddWithValue("@UserScore", userGame.UserScore);
                if (userGame.EnemyId != null)
                    myCommand.Parameters.AddWithValue("@EnemyId", userGame.EnemyId);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("User Game Details Updated Successfully");
        }

        /// <summary>
        /// Delete Game by Id
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.user_game
                             where user_game_id=@Id";
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

            return new JsonResult("Game Deleted Successfully");
        }
    }
}
