using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiselaWeb.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Deleted { get; set; }
    }
   }