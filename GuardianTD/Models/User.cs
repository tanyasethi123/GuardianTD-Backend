using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianTD.Models
{
    public class User
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First Name of the User
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name of the User
        /// </summary>
        public string LastName { get; set; }
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
