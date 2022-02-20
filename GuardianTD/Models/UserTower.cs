using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// User Tower Model
    /// </summary>
    public class UserTower
    {
        /// <summary>
        /// Id of the Tower of User
        /// </summary>
        public int UserTowerId { get; set; }
        /// <summary>
        /// Id of the User
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id of the Tower
        /// </summary>
        public int TowerId { get; set; }
    }
}
