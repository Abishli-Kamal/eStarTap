using Microsoft.AspNetCore.Identity;

namespace eStartap.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
