using Microsoft.AspNetCore.Identity;

namespace Buddle.Models
{
    public class User : IdentityUser
    {
        public required string FullName { get; set; }
    }
}
