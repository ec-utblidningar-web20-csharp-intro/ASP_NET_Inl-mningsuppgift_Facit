﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_Inlämningsuppgift_Facit.Models
{
    public class MyUser : IdentityUser
    {
        public string Role { get; set; }
        [InverseProperty("Attendees")]
        public List<Event> AttendingEvents { get; set; }
        [InverseProperty("Organizer")]
        public List<Event> OrganizedEvents { get; set; }
    }
}
