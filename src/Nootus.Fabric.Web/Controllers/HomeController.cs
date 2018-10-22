using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Nootus.Fabric.Web.Controllers
{
    public class HomeController: Controller
    {
        public ActionResult<string> ServiceStart()
        {
            return "Service Started";
        }
        public FileResult Index()
        {
            return File(System.IO.File.OpenRead(Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/index.html")), "text/html");
        }
    }
}
