using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.IdentityProvider
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public DateTime CompanyStarDate { get; set; }
    }
}
