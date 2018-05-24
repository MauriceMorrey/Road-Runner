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
            var currentUser = _context.users.Where(u => u.userId == (int)userId).Include(u => u.attended).Include(u => u.planned).SingleOrDefault();

            if (currentUser != null)
            {
                List<Trip> trips = _context.trips.Include(t => t.runners).ThenInclude(r => r.user).ToList();
                List<Friend> friendships = _context.friends.Where(f => f.senderId == currentUser.userId || f.receiverId == currentUser.userId).ToList();
                List<User> friends = new List<User>();
                foreach(var friend in friendships)
                {
                    if(friend.senderId == currentUser.userId)
                    {
                        friends.Add(_context.users.SingleOrDefault(u => friend.receiverId == u.userId));
                    }
                    else
                    {
                        friends.Add(_context.users.SingleOrDefault(u => friend.senderId == u.userId));
                    }
                }
                foreach(var user in friends)
                {
                    user.planned = _context.trips.Where(t => t.userId == user.userId).ToList();
                }
                ViewBag.ftrip = friends;
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
                // ViewBag.trip = trips;
                ViewBag.user = currentUser;
                ViewBag.runner = false;

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(TripViewModel plan)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.SingleOrDefault(u => u.userId == (int)userId);


            if (ModelState.IsValid)
            {
                Trip newTrip = new Trip()
                {
                    start_point = plan.start_point,
                    destination = plan.destination,
                    description = plan.description,
                    start_date = plan.start_date,
                    end_date = plan.end_date,
                    userId = currentUser.userId
                };

                _context.Add(newTrip);
                _context.SaveChanges();

                Runner newRunner = new Runner()
                {
                    userId = currentUser.userId,
                    tripId = newTrip.tripId
                };

                _context.Add(newRunner);
                _context.SaveChanges();



                return RedirectToAction("Dashboard");
            }
            return View("Create");

        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.SingleOrDefault(u => u.userId == (int)userId);

            if (currentUser != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int tripId)
        {
            Trip trip = _context.trips.SingleOrDefault(t => t.tripId == tripId);

            _context.Remove(trip);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("join")]
        public IActionResult Join(int tripId)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.SingleOrDefault(u => u.userId == (int)userId);

            Trip trip = _context.trips.SingleOrDefault(p => p.tripId == tripId);

            Runner joined = new Runner()
            {
                userId = currentUser.userId,
                tripId = trip.tripId
            };

            _context.Add(joined);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("leave")]
        public IActionResult Leave(int tripId)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.SingleOrDefault(u => u.userId == (int)userId);

            Trip trip = _context.trips.SingleOrDefault(p => p.tripId == tripId);
            Runner leaving = _context.runners.Where(v => v.tripId == trip.tripId && v.userId == currentUser.userId).SingleOrDefault();

            _context.Remove(leaving);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("show/{tripId}")]
        public IActionResult Show(int tripId)
        {

            Trip trip = _context.trips.Where(w => w.tripId == tripId).Include(p => p.runners).ThenInclude(v => v.user).SingleOrDefault();
            ViewBag.t = trip;

            return View();
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult UserPage(int userId)
        {

            int? uId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.SingleOrDefault(u => u.userId == (int)uId);

            if (uId == null)
            {
                return RedirectToAction("Index");
            }
            User thisUser = _context.users.Include(u => u.attended).Include(u => u.planned).SingleOrDefault(u => u.userId == userId);
            
            ViewBag.thisUser = thisUser;
            ViewBag.currentUser = currentUser;
            ViewBag.friends = false;
            Console.WriteLine(thisUser.sent.Count);
            Console.WriteLine(thisUser.received.Count);
            thisUser.sent = _context.friends.Where(f => f.senderId == thisUser.userId).ToList();
            thisUser.received = _context.friends.Where(f => f.receiverId == thisUser.userId).ToList();

            foreach (var friend in thisUser.sent)
            {
                if (friend.receiverId == currentUser.userId)
                {
                    ViewBag.friends = true;
                    Console.WriteLine($"sender id is{friend.senderId}");
                    Console.WriteLine($"receiver is{friend.receiverId}");
                    Console.WriteLine($"current user is{currentUser.userId}");
                    Console.WriteLine($"this user is{thisUser.userId}");
                    

                }
            }
            foreach (var friend in thisUser.received)
            {
                if (friend.senderId == currentUser.userId)
                {
                    ViewBag.friends = true;
                    Console.WriteLine($"sender id is{friend.senderId}");
                    Console.WriteLine($"receiver is{friend.receiverId}");
                    Console.WriteLine($"current user is{currentUser.userId}");
                    Console.WriteLine($"this user is{thisUser.userId}");
                }
            }
        
            
            Console.WriteLine(ViewBag.friends);
            return View("User");
        }

        [HttpPost]
        [Route("friend")]
        public IActionResult Friend(int friendAId, int friendBId)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            var currentUser = _context.users.SingleOrDefault(u => u.userId == (int)userId);
           
           if(friendBId == currentUser.userId)
           {
               return RedirectToAction("Dashboard");
           }
           Friend friend = new Friend()
           {
               senderId = currentUser.userId,
               receiverId = friendBId,
               accepted = false
           };

           _context.Add(friend);
           _context.SaveChanges();
       
            return RedirectToAction("Dashboard");
        }
    

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
