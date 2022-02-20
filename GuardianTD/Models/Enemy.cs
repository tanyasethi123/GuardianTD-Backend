using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// Enemy Model
    /// </summary>
    public class Enemy
    {
        /// <summary>
        /// Speed of the Enemy
        /// </summary>}
        public int EnemySpeed { get; set; }
        /// <summary>
        /// Id of the Enemy
        /// </summary>
        public int EnemyId { get; set; } 
        /// <summary>
        /// Max Health of the Enemy
        /// </summary>
        public int MaxHealth { get; set;  }
        /// <summary>
        /// Type of the Enemy
        /// </summary>
        public string EnemyType { get; set; }
        /// <summary>
        /// Level of the Enemy
        /// </summary>
        public int EnemyLevel { get; set; }
    }
}
