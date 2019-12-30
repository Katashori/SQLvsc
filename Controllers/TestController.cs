using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SQLvcs.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FileUploadApp.Controllers
{
    public class TestController : Controller
    {
        SQLvcsContext _context;
        IHostingEnvironment _appEnvironment;

        public TestController(SQLvcsContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(_context.Dacpacs.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Dacpac file = new Dacpac { DacpacName = uploadedFile.FileName, DacpacPath = path };
                _context.Dacpacs.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}