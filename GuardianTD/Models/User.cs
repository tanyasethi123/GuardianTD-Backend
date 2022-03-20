using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    /// <summary>
    /// User Model
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First Name of the User
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Email id of the User
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password of the User
        /// </summary>
        public string Password { get; set; }
    }
}
