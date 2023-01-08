using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Core.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.TeamFoundation.Common.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using TreeView.Data;
using TreeView.Models;

namespace TreeView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private CharacterDbContext Context { get; }

        public HomeController(ILogger<HomeController> logger, CharacterDbContext _context)
        {
            _logger = logger;
            Context = _context;
        }


        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();


            //Loop and add the Parent Nodes.
            foreach (CharacterType type in this.Context.CharacterType)
            {
                nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = "#", text = type.Character_name });
            }

            //Loop and add the Child Nodes.
            foreach (CharacterSubType subType in this.Context.CharacterSubType)
            {
                nodes.Add(new TreeViewNode { id = subType.Character_ParentId.ToString() + "-" + subType.Id.ToString(), parent = subType.Character_ParentId.ToString(), text = subType.Character_SubName });
            }

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }


        

        [HttpPost]
        public IActionResult Index(string selectedItems)
        {
            List<TreeViewNode>? items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);


            return Redirect("Index");
        }

        public ActionResult AddCustomer()
        {
            return View();
        }



        public IActionResult Privacy()
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