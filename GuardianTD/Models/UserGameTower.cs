using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
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
        public int TowerPositionX { get; set; }
        /// <summary>
        /// Position Y of Tower
        /// </summary>
        public int TowerPositionY { get; set; }
        /// <summary>
        /// Position Z of Tower
        /// </summary>
        public int TowerPositionZ { get; set; }
        /// <summary>
        /// Name of the Tower
        /// </summary>
        public string TowerName { get; set; }

    }
}
