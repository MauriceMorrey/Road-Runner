using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using road_runner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace road_runner.Controllers
{
    public class HomeController : Controller
    {
        private RoadRunnerContext _context;

        public HomeController(RoadRunnerContext context)
        {
            _context = context;

        }
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel user)
        {
            Register Reg = user.Reg;

            if (ModelState.IsValid)
            {
                PasswordHasher<Register> Hasher = new PasswordHasher<Register>();
                Reg.password = Hasher.HashPassword(Reg, Reg.password);

                User newUser = new User()
                {
                    first_name = Reg.first_name,
                    last_name = Reg.last_name,
                    username = Reg.username,
                    email = Reg.email,
                    password = Reg.password
                };

                _context.Add(newUser);
                _context.SaveChanges();

                User currentUser = _context.users.SingleOrDefault(u => u.email == newUser.email);
                HttpContext.Session.SetInt32("userId", currentUser.userId);

                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserViewModel user)
        {
            Login Log = user.Log;
            if (ModelState.IsValid)
            {
                var currentUser = _context.users.SingleOrDefault(u => u.email == Log.email);
                if (currentUser != null && Log.password != null)
                {
                    var Hasher = new PasswordHasher<User>();

                    if (0 != Hasher.VerifyHashedPassword(currentUser, currentUser.password, Log.password))
                    {
                        HttpContext.Session.SetInt32("userId", currentUser.userId);
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("Log.password", "Incorrect password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Log.email", "This email doesn't exist.");
                }
            }
            return View("Index");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.Where(u => u.userId == (int)userId).Include(u => u.attended).SingleOrDefault();

            if (currentUser != null)
            {
                List<Trip> trips = _context.trips.Include(t => t.runners).ThenInclude(r => r.user).ToList();
                foreach (var trip in trips)
                {
                    foreach (var runner in currentUser.attended)
                    {
                        if (runner.tripId == trip.tripId)
                        {
                            trip.currentUser = true;
                        }
                    }
                }
                ViewBag.trip = trips;
                ViewBag.user = currentUser;

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
