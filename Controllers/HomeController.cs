using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bazaar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Bazaar.Models.ViewModels;
using Bazaar.Models.DbModels;

namespace Bazaar.Controllers;

public class HomeController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<IdentityRole> roleManager) : Controller
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Copy data from RegisterViewModel to IdentityUser
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Fname = model.Fname,
                Lname = model.Lname,
                UserName = model.Email,
                PhoneNumber = model.Phone,
                Email = model.Email,
                AccoutCreationDate = DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss"),
                Agreement = model.Agreement
                
            };
            // Store user data in AspNetUsers database table
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded){
                var theuser = await _userManager.FindByIdAsync(user.Id);
                await _userManager.AddToRoleAsync(theuser, "user");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("index", "home");
            }
            else{
                return Content("Account couldnot be created");
            }

        }
        return View(model);
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Handle successful login
                return RedirectToAction("Index", "Home");
            }
            // Handle failure
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        // If we got this far, something failed, redisplay form
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "home");
    }



// public async Task<IActionResult> CreateRole(){
//             IdentityRole identityRole = new IdentityRole{Name = "admin"};
//             await _roleManager.CreateAsync(identityRole);
//             return RedirectToAction("Index", "Home");
// }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
