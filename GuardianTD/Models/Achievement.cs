using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// Achievement Model
    /// </summary>
    public class Achievement
    {
        /// <summary>
        /// Name of the Achievement
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Claim Type of the Achievement
        /// </summary>
        public string ClaimType { get; set; }
        /// <summary>
        /// Claim Value of the Achievement
        /// </summary>
        public int ClaimValue { get; set; }
    }
}
