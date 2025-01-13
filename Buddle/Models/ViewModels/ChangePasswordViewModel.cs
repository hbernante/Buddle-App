using System;
using System.ComponentModel.DataAnnotations;

namespace Buddle.Models.ViewModels;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(40, ErrorMessage = "The password must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Compare("ConfirmNewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm New Password")]
    public string ConfirmNewPassword { get; set; }
}
