using Microsoft.AspNetCore.Identity;
using System;
using System.Text;

namespace Model.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string AboutUs { get; set; }
        public string Image { get; set; }

        // /#/users/eduardo-15
        public string SeoUrl { get; set; }
    }
}
