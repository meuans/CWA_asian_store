using CWA_asian_store.Entity.Model;
using CWA_asian_store.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CWA_asian_store.Controllers
{


    public class StoreController : Controller
    {
        private readonly IProductService _productService;

        public StoreController(IProductService productService)
        {
            _productService = productService;
        }




        public async Task<IActionResult> Index(string? search, string? sortOrder, int pageNumber = 1)
        {
            int pageSize = 5; // кількість товарів на сторінку
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;

            // Отримуємо відсортовані та відфільтровані товари
            var products = await _productService.SearchAsync(search, sortOrder);
          

            // Пагінація
            int totalItems = products.Count;
            var paginatedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(paginatedProducts);
        }






        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _productService.GetAllCategoriesAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            await _productService.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(
             await _productService.GetAllCategoriesAsync(),
             "Id",
             "Name",
             product.CategoryId
         );

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _productService.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }





        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

  


    }


}