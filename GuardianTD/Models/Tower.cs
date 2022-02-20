using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// Tower Model
    /// </summary>
    public class Tower
    {
        /// <summary>
        /// Id of the Tower
        /// </summary>
        public int TowerId { get; set; }
        /// <summary>
        /// Range of the Tower
        /// </summary>
        public int TowerRange { get; set; }
        /// <summary>
        /// Type of the Tower
        /// </summary>
        public string TowerType { get; set; }
        /// <summary>
        /// Name of the Tower
        /// </summary>
        public string TowerName { get; set; }
        /// <summary>
        /// Damage caused by the Tower
        /// </summary>
        public int TowerDamage { get; set; }
        /// <summary>
        /// Value of the Tower
        /// </summary>
        public int TowerValue { get; set; }
    }
}
