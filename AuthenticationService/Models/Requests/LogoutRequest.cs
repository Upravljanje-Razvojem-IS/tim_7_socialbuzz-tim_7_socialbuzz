using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models.Requests
{
    public class LogoutRequest
    {
        /// <summary>
        /// Token of account that wants to logout.
        /// </summary>
        public string Token { get; set; }
    }
}
