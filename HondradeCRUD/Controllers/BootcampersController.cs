using HondradeCRUD.Models;
using HondradeCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HondradeCRUD.Controllers
{
    public class BootcampersController : Controller
    {

        private readonly DBContext context;
        private readonly IWebHostEnvironment environment;

        public BootcampersController(DBContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment=environment;
        }


        public IActionResult Index()
        {
            var bootcampers = context.Bootcampers.ToList();
            return View(bootcampers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BootcamperDTO bootcamperDto)
        {
            if (bootcamperDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(bootcamperDto);
            }


            DateTime today = DateTime.Today;
            int age = today.Year - bootcamperDto.Birthday.Year;
            if (bootcamperDto.Birthday.Date > today.AddYears(-age))
            {
                age--;
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(bootcamperDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/profiles/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                bootcamperDto.ImageFile.CopyTo(stream);
            }

            Bootcamper bootcamper = new Bootcamper()
            {
                Name = bootcamperDto.Name,
                Email = bootcamperDto.Email,
                Phone = bootcamperDto.Phone,
                Birthday = bootcamperDto.Birthday,
                Age = age,
                Address = bootcamperDto.Address,
                Gender = bootcamperDto.Gender,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };

            context.Bootcampers.Add(bootcamper);
            context.SaveChanges();

            TempData["SuccessMessage"] = "Bootcamp created successfully.";
            return RedirectToAction("Index", "Bootcampers");
        }

        public IActionResult Edit(int id)
        {

            var bootcamper = context.Bootcampers.Find(id);

            if (bootcamper == null)
            {
                return RedirectToAction("Index", "Bootcampers");
            }

            DateTime today = DateTime.Today;
            int age = today.Year - bootcamper.Birthday.Year;
            if (bootcamper.Birthday.Date > today.AddYears(-age))
            {
                age--;
            }

            var bootcamperDto = new BootcamperDTO()
            {
                Name = bootcamper.Name,
                Email = bootcamper.Email,
                Phone = bootcamper.Phone,
                Birthday = bootcamper.Birthday,
                Age = age,
                Address = bootcamper.Address,
                Gender = bootcamper.Gender,
            };

            ViewData["BootcamperId"] = bootcamper.Id;
            ViewData["ImageFileName"] = bootcamper.ImageFileName;
            ViewData["CreatedAt"] = bootcamper.CreatedAt.ToString("MM/dd/yyyy");

            return View(bootcamperDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, BootcamperDTO bootcamperDto)
        {
            var bootcamper = context.Bootcampers.Find(id);

            if (bootcamper == null)
            {
                return RedirectToAction("Index", "Bootcampers");
            }

            DateTime today = DateTime.Today;
            int age = today.Year - bootcamperDto.Birthday.Year;
            if (bootcamperDto.Birthday.Date > today.AddYears(-age))
            {
                age--;
            }

            if (!ModelState.IsValid)
            {
                ViewData["BootcamperId"] = bootcamper.Id;
                ViewData["ImageFileName"] = bootcamper.ImageFileName;
                ViewData["CreatedAt"] = bootcamper.CreatedAt.ToString("MM/dd/yyyy");

                return View(bootcamperDto);
            }

            string newFileName = bootcamper.ImageFileName;
            if (bootcamperDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(bootcamperDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/profiles/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    bootcamperDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/profiles/" + bootcamper.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }


            bootcamper.Name = bootcamperDto.Name;
            bootcamper.Email = bootcamperDto.Email;
            bootcamper.Phone = bootcamperDto.Phone;
            bootcamper.Birthday = bootcamperDto.Birthday;
            bootcamper.Age = age;
            bootcamper.Address = bootcamperDto.Address;
            bootcamper.Gender = bootcamperDto.Gender;
            bootcamper.ImageFileName = newFileName;


            context.SaveChanges();

            TempData["SuccessMessage"] = "Bootcamper updated successfully.";
            return RedirectToAction("Index", "Bootcampers");
        }

        public IActionResult Delete(int id)
        {
            var bootcamper = context.Bootcampers.Find(id);
            if (bootcamper == null)
            {
                return RedirectToAction("Index", "Bootcampers");
            }
            string imageFullPath = environment.WebRootPath + "/profiles" + bootcamper.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Bootcampers.Remove(bootcamper);
            context.SaveChanges(true);

            TempData["SuccessMessage"] = "Bootcamper deleted successfully.";
            return RedirectToAction("Index", "Bootcampers");
        }

        public IActionResult Update(string query, string gender, string sort)
        {
            var lowerQuery = (query ?? string.Empty).ToLower();
            gender = gender ?? "All";

            IQueryable<Bootcamper> bootcampers = context.Bootcampers;

            if (!string.IsNullOrEmpty(query))
            {
                bootcampers = bootcampers.Where(p => p.Name.ToLower().Contains(lowerQuery));
            }

            if (gender != "All")
            {
                bootcampers = bootcampers.Where(p => p.Gender == gender);
            }

            if (sort == "asc")
            {
                bootcampers = bootcampers.OrderBy(p => p.Name);
            }
            else if (sort == "desc")
            {
                bootcampers = bootcampers.OrderByDescending(p => p.Name);
            }

            var resultBootcampers = bootcampers.ToList();

            return PartialView("Shared/_BootcamperList", resultBootcampers);
        }

    }
}
