using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Web;
using System.IO;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product )
        {
            if (product != null && ModelState.IsValid) { 
            
                if (this.Request.Files != null && this.Request.Files.Count != 0 && this.Request.Files[0].ContentLength > 0)
                {
                    string fileName = Path.GetFileName(this.Request.Files[0].FileName); // got the file name
                    string filePathOfWebsite = "~/Images/Covers" + fileName;
                    product.CoverImagePath = filePathOfWebsite; //Save web path in database
                    this.Request.Files[0].SaveAs(this.Server.MapPath(filePathOfWebsite));
                }
                else{
                    product.CoverImagePath = "~/Images/Covers/DefaultCover.jpg";
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ActionResult Delete(int id)
        {
            Product deletedProduct = repository.DeleteProduct(id);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == id);
            return View(product);
                   }
    }
}