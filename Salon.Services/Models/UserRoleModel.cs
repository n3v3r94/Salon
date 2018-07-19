using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Services.Models
{
    public class UserRoleModel
    {
        public string UserName { get; set; }

        public string id { get; set; }

        public IList<string> RoleUser {get;set;}
    }
}
