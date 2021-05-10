using ApniShop.Models;
using ApniShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Services
{
    public interface IProductServices
    {
        public List<ProductViewModel> GetProductVM();
        public List<ProductViewModel> GetProductVMById(string id);
        public List<ProductViewModel> GetProductVMByStatus(bool status = false);

    }
}
