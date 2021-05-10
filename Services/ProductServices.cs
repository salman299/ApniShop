using ApniShop.Models;
using ApniShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Services
{
    public class ProductServices: IProductServices
    {
        private readonly ApplicationDbContext _db;

        public ProductServices(ApplicationDbContext db)
        {
            _db = db;
        }

        //  public async Task<string> getUserById (string userId) {

        //  }

       
        public  List<ProductViewModel> GetProductVM()
        {
            //join admin in _db.Users on product.AdminID equals admin.Id
            
            var products = _db.Products.ToList();
            var productVMList=  (from product in _db.Products         
             join user in _db.Users on product.UserID equals user.Id
             join admin in _db.Users on product.AdminID equals admin.Id into dept from department in dept.DefaultIfEmpty()
             select new ProductViewModel
             {
                 ID = product.ID,
                 Name = product.Name,
                 Description = product.Description,
                 ImageUrl = product.ImageUrl,
                 IsAdminApproved = product.IsAdminApproved,
                 IsDeploy = product.IsDeployed,
                 Price = product.Price,
                 UserID = product.UserID,
                 AdminID = product.AdminID,
                 UserName = user.Name,
                 AdminName = department.Name,
             }
             ).ToList();

           

            return productVMList;
        }


        public List<ProductViewModel> GetProductVMByStatus(bool status=false)
        {
            //join admin in _db.Users on product.AdminID equals admin.Id

            var products = _db.Products.ToList();
            var productVMList = (from product in _db.Products
                                 where product.IsAdminApproved == status
                                 join user in _db.Users on product.UserID equals user.Id
                                 join admin in _db.Users on product.AdminID equals admin.Id into dept
                                 from department in dept.DefaultIfEmpty()
                                 select new ProductViewModel
                                 {
                                     ID = product.ID,
                                     Name = product.Name,
                                     Description = product.Description,
                                     ImageUrl = product.ImageUrl,
                                     IsAdminApproved = product.IsAdminApproved,
                                     IsDeploy = product.IsDeployed,
                                     Price = product.Price,
                                     UserID = product.UserID,
                                     AdminID = product.AdminID,
                                     UserName = user.Name,
                                     AdminName = department.Name,
                                 }
             ).ToList();



            return productVMList;
        }
        public List<ProductViewModel> GetProductVMById(string id)
        {
            //join admin in _db.Users on product.AdminID equals admin.Id

            var products = _db.Products.ToList();
            var productVMList = (from product in _db.Products
                                 where product.UserID==id
                                 join user in _db.Users on product.UserID equals user.Id
                                 join admin in _db.Users on product.AdminID equals admin.Id into dept
                                 from department in dept.DefaultIfEmpty()
                               
                                 select new ProductViewModel
                                 {
                                     ID = product.ID,
                                     Name = product.Name,
                                     Description = product.Description,
                                     ImageUrl = product.ImageUrl,
                                     IsAdminApproved = product.IsAdminApproved,
                                     IsDeploy = product.IsDeployed,
                                     Price = product.Price,
                                     UserID = product.UserID,
                                     AdminID = product.AdminID,
                                     UserName = user.Name,
                                     AdminName = department.Name,
                                 }
             ).ToList();
            return productVMList;
        }


    }
}
