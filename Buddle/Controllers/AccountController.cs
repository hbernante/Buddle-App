using Buddle.Models;
using Buddle.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Buddle.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Login()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User users = new User
                {
                    FullName = model.FirstName + " " + model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    // Redirect to the Hobby page to collect additional information
                    return RedirectToAction("Hobby", new { email = model.Email });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        // GET: Hobby
        public IActionResult Hobby(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Register", "Account");
            }

            var user = userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                var hobbyModel = new HobbyViewModel
                {
                    Email = user.Email,
                };
                return View(hobbyModel);
            }

            return RedirectToAction("Register", "Account");
        }

        // POST: Hobby
        [HttpPost]
        public async Task<IActionResult> Hobby(HobbyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Handle profile image upload
                    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                    {
                        // Check if the file is an image by looking at its content type
                        var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };
                        if (!allowedContentTypes.Contains(model.ProfileImage.ContentType))
                        {
                            ModelState.AddModelError("ProfileImage", "Only image files (JPG, PNG, GIF, BMP, WebP) are allowed.");
                            return View(model);
                        }

                        // Convert the uploaded file to a byte array
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ProfileImage.CopyToAsync(memoryStream);
                            user.ProfileImage = memoryStream.ToArray();
                        }
                    }

                    // Update user fields
                    user.Birthday = model.Birthday;
                    user.Gender = model.Gender;
                    user.Hobbies = model.Hobbies != null ? string.Join(", ", model.Hobbies) : null;
                    user.Latitude = model.Latitude;
                    user.Longitude = model.Longitude;

                    // Save changes to the database
                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        // Automatically sign in the user after updating the profile
                        await signInManager.SignInAsync(user, isPersistent: false);

                        // Redirect to the home page
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                }
            }

            // Return to the view with the same model (including email) on validation failure
            return View(model);
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
