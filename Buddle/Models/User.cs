using Microsoft.AspNetCore.Identity;

namespace Buddle.Models
{
    public class User : IdentityUser
    {
        public required string FullName { get; set; }
        public DateTime Birthday { get; set; }

        public string Gender { get; set; } = "Not Specified"; // Placeholder value

        public string Hobbies { get; set; } = "None"; // Placeholder value

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Add this property for storing raw image data
        public byte[]? ProfileImage { get; set; } // Renamed property
    }
}
