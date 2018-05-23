using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationProject.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using ConfigurationProject.Models;

namespace ConfigurationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationRepository _repo;
       
        public HomeController(IConfigurationRepository repository)
        {
            _repo = repository;
           
        }
        public IActionResult Index()
        {
            var result = _repo.Repository.All().ToList();
            return View(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Application()
        {
            var result = _repo.Repository.All().ToList();
            return View(result);
        }
    }
}
