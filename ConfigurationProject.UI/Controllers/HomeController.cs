using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using ConfigurationProject.UI.Models;

namespace ConfigurationProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationReader _configReader;

        public HomeController(IConfigurationReader configReader)
        {
            _configReader = configReader;
        }

        public IActionResult Index()
        {
            var configItem = _configReader.GetValue<string>("SiteName");
            var configModel = new ProjectConfigModel
            {
                Name = "SiteName",
                Value = configItem
            };
            // bu değer aktif olmadığından gelmemeli
            var configItem2 = _configReader.GetValue<int?>("MaxItemCount");

            return View(configModel);
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
    }
}
