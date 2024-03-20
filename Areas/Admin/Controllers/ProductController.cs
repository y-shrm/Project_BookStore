using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Project_BookStore.DataAccess.Data;
using Project_BookStore.DataAccess.Repository;
using Project_BookStore.Models;
using Project_BookStore.Models.ViewModels;
using System.Collections.Generic;

namespace Project_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryRepository _categoryRepo;

        //public ProductController(IProductRepository db)
        //{
        //    _productRepo = db;

        //}
        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Index()
        {
            List<Product> products = _productRepo.GetAll(includeProperties:"Category").ToList();
        
            return View(products);
            
        }



        public IActionResult UpSert(int? id)
        {
            ProductVM productVM = new()
            {
                categoryList = _categoryRepo.GetAll().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Product = new Product()
            };
            if(id == null || id == 0){

                return View(productVM);
            }
            else
            {
                productVM.Product = _productRepo.Get(u => u.Id == id);
                return View(productVM);

            }
         
        }


        [HttpPost]
        public IActionResult UpSert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                   string filename =Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                   string productPath = Path.Combine(wwwRootPath, @"images/product");

                    

                    if (!string.IsNullOrEmpty(productVM.Product.ImageURL))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);    
                    }

                    productVM.Product.ImageURL = @"\images\product\" + filename;
                }

                if(productVM.Product.Id ==0) {
                    _productRepo.Add(productVM.Product);

                }
                _productRepo.Update(productVM.Product);
                _productRepo.Save();

                return RedirectToAction("Index");
            }
            else
            {
                productVM.categoryList = _categoryRepo.GetAll().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name

                });
                return View(productVM);
            }
        }





        //[HttpPost]
        //public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (productVM.Product.Id == 0)
        //        {
        //            _productRepo.Add(productVM.Product);
        //        }
        //        else
        //        {
        //            _productRepo.Update(productVM.Product);
        //        }
        //        _productRepo.Save();


        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if (files != null)
        //        {

        //            foreach (IFormFile file in files)
        //            {
        //                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //                string productPath = @"images\products\product-" + productVM.Product.Id;
        //                string finalPath = Path.Combine(wwwRootPath, productPath);

        //                if (!Directory.Exists(finalPath))
        //                    Directory.CreateDirectory(finalPath);

        //                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
        //                {
        //                    file.CopyTo(fileStream);
        //                }

        //                ProductImage productImage = new()
        //                {
        //                    ImageUrl = @"\" + productPath + @"\" + fileName,
        //                    ProductId = productVM.Product.Id,
        //                };

        //                if (productVM.Product.ProductImages == null)
        //                    productVM.Product.ProductImages = new List<ProductImage>();

        //                productVM.Product.ProductImages.Add(productImage);

        //            }

        //            _unitOfWork.Product.Update(productVM.Product);
        //            _unitOfWork.Save();




        //        }


        //        TempData["success"] = "Product created/updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        productVM.categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString()
        //        });
        //        return View(productVM);
        //    }
        //}


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? FindCategory = _productRepo.Get(u => u.Id == id);

            if (FindCategory == null)
            {
                return NotFound();
            }
            return View(FindCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _productRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _productRepo.Remove(obj);
            _productRepo.Save();

            return RedirectToAction("Index");


        }
    }
}
