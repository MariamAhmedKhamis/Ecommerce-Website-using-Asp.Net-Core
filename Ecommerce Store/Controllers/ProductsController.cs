using Ecommerce_Store.DbContexts;
using Ecommerce_Store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetIndexView()
        {
            return View("Index", _context.Products.ToList());
        }

        [HttpGet]  
        public IActionResult GetCreatView()
        {
            ViewBag.AllSections = _context.Products.ToList();
            return View("Create");
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
                return NotFound();
            else
                return View( "Details", product);
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddNew(Product product)
        {
            if (ModelState.IsValid == true)
            {
                if (product.ImageFile == null)
                {
                    product.ImagePath = "\\images\\No_Image.png";
                }
                else
                {
                    Guid imageGuid = Guid.NewGuid();
                    string imageExtension = Path.GetExtension(product.ImageFile.FileName);
                    product.ImagePath = "\\images\\" + imageGuid + imageExtension;
                    string imageUploadPath = _webHostEnvironment.WebRootPath + product.ImagePath;

                    FileStream imagestream = new FileStream(imageUploadPath, FileMode.Create);
                    imagestream.Dispose();
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllDepartments = _context.Sections.ToList();
                return View("Create");
            }
        }


        public IActionResult GetEditView(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", product);
            }
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditCurrent(Product product)
        {
            if (ModelState.IsValid == true)
            {
                if (product.ImageFile != null)
                {
                    if (product.ImagePath != "\\images\\No_Image.png")
                    {
                        System.IO.File.Delete(_webHostEnvironment.WebRootPath + product.ImagePath);
                    }
                    Guid imageGuid = Guid.NewGuid();
                    string imageExtension = Path.GetExtension(product.ImageFile.FileName);
                    product.ImagePath = "\\images\\" + imageGuid + imageExtension;
                    string imageUploadPath = _webHostEnvironment.WebRootPath + product.ImagePath;

                    FileStream imagestream = new FileStream(imageUploadPath, FileMode.Create);
                    product.ImageFile.CopyTo(imagestream);
                    imagestream.Dispose();
                }
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllDepartments = _context.Sections.ToList();
                return View("Edit");
            }
        }


        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", product);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Product product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            else
            {
                if (product.ImagePath != "\\images\\No_Image.png")
                {
                    System.IO.File.Delete(_webHostEnvironment.WebRootPath + product.ImagePath);
                }
                _context.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
        }







    }
}
