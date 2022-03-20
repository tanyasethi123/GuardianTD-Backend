using GuardianTD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Controllers
{
    /// <summary>
    /// Controller for the Achievements APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public AchievementsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get All Achievement Details
        /// </summary>
        /// <returns>Returns all Achievement Details</returns>
        [HttpGet]
        public JsonResult GetAll()
        {
            string query = @"select * from dbo.achievement";
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
        /// Add Achievement
        /// </summary>
        /// <param name="achievement">User Achievement object with details</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(Achievement achievement)
        {
            string query = @"
                            insert into dbo.achievement
                            ([name],[claim_type],[claim_value])
                            values (@Name,@ClaimType,@ClaimValue)";
            string sqlDataSource = _configuration.GetConnectionString("GuardianTDConn");
            SqlDataReader myReader;
            DataTable table = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@Name", achievement.Name);
                myCommand.Parameters.AddWithValue("@ClaimType", achievement.ClaimType);
                myCommand.Parameters.AddWithValue("@ClaimValue", achievement.ClaimValue);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult("Achievement Added Successfully");
        }
    }
}
