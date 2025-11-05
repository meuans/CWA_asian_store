using CWA_asian_store.Entity.Model;
using CWA_asian_store.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


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

            // Беремо всі товари
            var products = await _productService.GetAllAsync();

            // Фільтрація за назвою
            if (!string.IsNullOrEmpty(search))
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            // Сортування
            products = sortOrder switch
            {
                "asc" => products.OrderBy(p => p.Price).ToList(),
                "desc" => products.OrderByDescending(p => p.Price).ToList(),
                _ => products
            };

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



        // Відображає форму для додавання товару
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Приймає дані з форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            Console.WriteLine($"[DEBUG] Name={product.Name}, Desc={product.Description}, Category={product.Category}, Price={product.Price}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[DEBUG] ModelState INVALID");
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