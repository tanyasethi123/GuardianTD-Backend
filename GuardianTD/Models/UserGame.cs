using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    public class UserGame
    {
        /// <summary>
        /// Id of the User Game
        /// </summary>
        public int UserGameId { get; set; }
        /// <summary>
        /// Id of the User
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Type of Game
        /// </summary>
        public string GameType { get; set; }
        /// <summary>
        /// Score of the User
        /// </summary>
        public int? UserScore { get; set; }
        /// <summary>
        /// Id of the Enemy
        /// </summary>
        public int? EnemyId { get; set; }
    }
}
