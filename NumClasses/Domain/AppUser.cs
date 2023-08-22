

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumClass.Domain
{
    public class AppUser: IdentityUser
    {
        // i used IdentityClass that includes many properties like id, usernake, email ...etc
        // added two more 
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
