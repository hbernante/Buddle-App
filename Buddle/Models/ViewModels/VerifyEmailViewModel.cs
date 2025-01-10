using System;
using System.ComponentModel.DataAnnotations;

namespace Buddle.Models.ViewModels;

public class VerifyEmailViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }
}
