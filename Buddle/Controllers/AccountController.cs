using Microsoft.AspNetCore.Mvc;

namespace Buddle.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    public IActionResult ResetPassword()
    {
        return View();
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult UpdateProfile()
    {
        return View();
    }

    public IActionResult UpdatePassword()
    {
        return View();
    }

    public IActionResult UpdateEmail()
    {
        return View();
    }

    public IActionResult UpdatePhoneNumber()
    {
        return View();
    }

    public IActionResult ConfirmEmail()
    {
        return View();
    }

    public IActionResult ConfirmPhoneNumber()
    {
        return View();
    }

    public IActionResult ConfirmAccount()
    {
        return View();
    }

    public IActionResult Lockout()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return View();
    }
}
