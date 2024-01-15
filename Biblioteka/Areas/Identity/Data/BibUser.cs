using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BibUser class
public class BibUser : IdentityUser
{
    public string name { get; set; }
    public string surname { get; set; }
    public DateTime? birthDate { get; set; }
}

