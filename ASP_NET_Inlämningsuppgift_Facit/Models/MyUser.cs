using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_Inlämningsuppgift_Facit.Models
{
    public class MyUser : IdentityUser
    {
        public List<Event> MyEvents { get; set; }
    }
}
