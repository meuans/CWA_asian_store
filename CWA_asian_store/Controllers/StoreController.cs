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

        // Показує всі товари
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
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
        [HttpPost]
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




    }


}