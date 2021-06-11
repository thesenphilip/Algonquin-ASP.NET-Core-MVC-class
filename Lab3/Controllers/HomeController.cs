using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {

            return View();
        }

        public IActionResult Razor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Count()
        {
            ViewData["Bottles"] = Request.Form["Bottles"];
            return View();
        }

        [HttpGet]
        public IActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DisplayPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                return View(person);
            }

            return View("Error");

        }


    }
}
