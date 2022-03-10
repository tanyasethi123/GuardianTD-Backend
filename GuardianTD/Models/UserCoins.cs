using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// User Coins Model
    /// </summary>
    public class UserCoins
    {
        /// <summary>
        /// Id of the Coins of User
        /// </summary>
        public int UserCoinsId { get; set; }
        /// <summary>
        /// Id of the User
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Type of Coins
        /// </summary>
        public string CoinType { get; set; }
        /// <summary>
        /// Number of  Coins
        /// </summary>
        public int NumberOfCoins{ get; set;}
    }
}
