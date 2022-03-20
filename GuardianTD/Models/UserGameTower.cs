using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// User Game Tower Model
    /// </summary>
    public class UserGameTower
    {
        /// <summary>
        /// Id of the Game Tower of the User
        /// </summary>
        public int UserGameTowerId { get; set; }
        /// <summary>
        /// Id of the Game of User
        /// </summary>
        public int UserGameId { get; set; }
        /// <summary>
        /// Id of the Tower
        /// </summary>
        public int TowerId { get; set; }
        /// <summary>
        /// Position X of Tower
        /// </summary>
        public string Vector { get; set; }
        /// <summary>
        /// Name of the Tower
        /// </summary>
        public string TowerName { get; set; }

    }
}
