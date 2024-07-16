using HondradeCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace HondradeCRUD.Controllers
{
    public class BootcampersController : Controller
    {

        private readonly DBContext context;

        public BootcampersController(DBContext context)
        {
            this.context = context; 
        }


        public IActionResult Index()
        {
            var bootcampers = context.Bootcampers.ToList();
            return View(bootcampers);
        }
    }
}
