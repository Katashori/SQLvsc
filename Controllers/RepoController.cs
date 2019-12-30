using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SQLvcs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SQLvcs.Controllers
{
    public class RepoController : Controller
    {
        private readonly SQLvcsContext _context;

        public RepoController(SQLvcsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            //цикл для клиентов.
            foreach (Client client in _context.Clients)
            {
                nodes.Add(new TreeViewNode { id = client.ClientId.ToString(), parent = "#", text = client.ClientName });
                string clientparent = client.ClientId.ToString();
                //цикл для проектов с этого клиента
                foreach (Project project in _context.Projects.Where(a => a.ClientId.Equals(client.ClientId)))
                {
                    nodes.Add(new TreeViewNode { id = client.ClientId.ToString() + "-" + project.ProjectId.ToString(), parent = project.ClientId.ToString(), text = project.ProjectName });
                    string projectparent = client.ClientId.ToString() + "-" + project.ProjectId.ToString();
                    //цикл для инстансов этого проекта
                    foreach (Instance instance in _context.Instances.Where(b => b.ProjectId.Equals(project.ProjectId)))
                    {
                        nodes.Add(new TreeViewNode { id = client.ClientId.ToString() + "-" + project.ProjectId.ToString() + "-" + instance.InstanceId.ToString(), parent = client.ClientId.ToString() + "-" + project.ProjectId.ToString(), text = instance.InstanceName });
                        string instanceparent = client.ClientId.ToString() + "-" + project.ProjectId.ToString() + "-" + instance.InstanceId.ToString();
                        //цикл для инстансов этого инстанса
                        foreach (Database database in _context.Databases.Where(c => c.InstanceId.Equals(instance.InstanceId)))
                        {
                            nodes.Add(new TreeViewNode { id = client.ClientId.ToString() + "-" + project.ProjectId.ToString() + "-" + instance.InstanceId.ToString() + "-" + database.DatabaseId.ToString(), parent = client.ClientId.ToString() + "-" + project.ProjectId.ToString() + "-" + instance.InstanceId.ToString(), text = database.DatabaseName });
                            string databaseparent = client.ClientId.ToString() + "-" + project.ProjectId.ToString() + "-" + instance.InstanceId.ToString();

                        }
                    }
                }
            }

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
        }

        public ActionResult Manage()
        {
            if (_context.Clients.Count() > 0)
            {
                var selectedIndex = _context.Clients.First().ClientId;
                //int selectedIndex = 1;
                SelectList clients = new SelectList(_context.Clients, "ClientId", "ClientName", selectedIndex);
                ViewBag.Clients = clients;
            }

            return View();
        }

        public ActionResult GetDacpacs(int id)
        {
            return PartialView(_context.Dacpacs.Where(c => c.DatabaseId == id).ToList());
        }
        public ActionResult GetDatabases(int id)
        {
            return PartialView(_context.Databases.Where(c => c.InstanceId == id).ToList());
        }
        public ActionResult GetInstances(int id)
        {
            return PartialView(_context.Instances.Where(c => c.ProjectId == id).ToList());
        }

        public ActionResult GetProjects(int id)
        {
            return PartialView(_context.Projects.Where(c => c.ClientId == id).ToList());
        }

        public ActionResult GetClients()
        {
            return PartialView(_context.Clients.ToList());
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}