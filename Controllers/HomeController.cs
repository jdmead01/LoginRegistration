using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Http;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpPost("register")]

        public IActionResult Register(IndexViewModel form)
        {
            Register registerForm = form.newRegister;
            if (ModelState.IsValid){
                dbContext.Add(registerForm);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
            return View("Index");
        }
        [HttpPost("login")]
        public IActionResult Login(IndexViewModel form)
        {
            Login loginForm = form.newLogin;
            if (ModelState.IsValid){
                return RedirectToAction("Success");
            }
            return View("Index");
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
