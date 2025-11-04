using CWA_asian_store.Entity.Model;
using CWA_asian_store.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CWA_asian_store.Controllers
{


    public class StoreController : Controller
    {
        private readonly IProductService _productService;

        public StoreController(IProductService productService)
        {
            _productService = productService;
        }

        // Отримує всі продукти через сервіс і повертає View для показу списку
  
        public async Task<IActionResult> Index(string? search)
        {
            var products = await _productService.GetAllAsync();

            // Якщо є рядок пошуку — фільтруємо товари
            if (!string.IsNullOrEmpty(search))
            {
                products = products
                    .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search; // щоб зберігати значення в полі пошуку

            return View(products);
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

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


    }


}