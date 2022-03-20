using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    public class UserAchievement
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id of the Achievement
        /// </summary>
        public int AchievementId { get; set; }
    }
}
