using ApniShop.Models;
using ApniShop.Models.ViewModels;
using ApniShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApniShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly  IHttpContextAccessor _httpContextAccessor;
        private readonly string _loginUserId;
        private readonly string _role;
        private readonly IProductServices _productServices;


        public ProductController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IProductServices productServices) {
            _db = db;

            _productServices = productServices;

            _httpContextAccessor = httpContextAccessor;
         
            _loginUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _role = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

        }

        public IActionResult Index()
        {
            return View(_productServices.GetProductVM());
        }

        public IActionResult PendingProducts()
        {
            return View(_productServices.GetProductVMByStatus(false));
        }

        public IActionResult ApprovedProducts()
        {
            return View(_productServices.GetProductVMByStatus(true));
        }


        public IActionResult MyProducts()
        {

            return View(_productServices.GetProductVMById(_loginUserId));

        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid) {
                var product = new Product()
                {
                    Name = model.Name,
                    Description= model.Description,
                    ImageUrl= model.ImageUrl,
                    Price= model.Price,
                    UserID= _loginUserId,
                    IsAdminApproved= model.IsAdminApproved,
                    IsDeployed= model.IsDeploy,
                    AdminID= model.AdminID,
                };
                _db.Add<Product>(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products.FindAsync(id);
            var productVM = new ProductViewModel
            {
                ID = product.ID,
                Description=product.Description,
                Price=product.Price,
                Name=product.Name,
                ImageUrl=product.ImageUrl,
                IsDeploy=product.IsDeployed,
                IsAdminApproved=product.IsAdminApproved,
                UserID=product.UserID,
                AdminID=product.AdminID,
            };
            if (product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productVM)
        {
            if (id != productVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = new Product {
                        ID = id,
                        ImageUrl=productVM.ImageUrl,
                        IsDeployed=productVM.IsDeploy,
                        Price=productVM.Price,
                        Description=productVM.Description,
                        Name=productVM.Name,
                        UserID=productVM.UserID,
                        AdminID=productVM.AdminID,
                        IsAdminApproved=productVM.IsAdminApproved,
                        
                    };

                    _db.Update<Product>(product);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index","Product");
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                
            }
            return View(productVM);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveProduct(string[] ids) {

            if (ids == null || ids.Length == 0)
           
            {
                ModelState.AddModelError("", "No item selected to delete");   
                return View();
            }
            List<int> TaskIds = ids.Select(x => Int32.Parse(x)).ToList();
            foreach (int id in TaskIds) {
                var product = await _db.Products.FindAsync(id);
                product.AdminID = _loginUserId;
                product.IsAdminApproved = true;
                if (await TryUpdateModelAsync<Product>(product,"product",s => s.AdminID, s => s.IsAdminApproved))
                {

                    // EF will detect the change and update only the column that has changed.
                    await _db.SaveChangesAsync();

                }
               // _db.Entry(product).Property(x => x.AdminID).IsModified = false;
               // _db.Entry(product).Property(x => x.IsDeployed).IsModified = true;
            }
            

            //foreach (string id in ids)
            // {
            //    var employee = _db.Employees.Find(int.Parse(id));
            //   this.db.Employees.Remove(employee);
            //    this.db.SaveChanges();
            //}
            return RedirectToAction("Index");
        }
    }
}
