using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Buddle.Models.ViewModels
{
    public class HobbyViewModel
    {
        public string Email { get; set; }

        // Birthday
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        // Gender
        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        // Hobbies
        [Required(ErrorMessage = "Hobbies are required")]
        [Display(Name = "Hobbies")]
        public List<string> Hobbies { get; set; } = new();

        // Latitude (Location)
        [Required(ErrorMessage = "Latitude is required")]
        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        // Longitude (Location)
        [Required(ErrorMessage = "Longitude is required")]
        [Range(-180.0, 180.0, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        // New property for profile image upload
        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }

        public string ProfileImagePreviewPath { get; set; } = "/images/sphere.png";
    }
}
