using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_Inlämningsuppgift_Facit.Models
{
    public enum UserRole
    {
        User,
        Admin,
        Organizer
    };
    public class MyUser : IdentityUser
    {
        public UserRole Role { get; set; }
        [InverseProperty("Attendees")]
        public List<Event> AttendingEvents { get; set; }
        [InverseProperty("Organizer")]
        public List<Event> OrganizedEvents { get; set; }
    }
}
