using finalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace finalProject.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Inventory()
        {
            var products = _context.Junk.ToList();
            var totalValue = products.Sum(p => p.Price * p.Quantity);
            var productCount = products.Count;

            ViewData["TotalValue"] = totalValue;
            ViewData["ProductCount"] = productCount;

            return View(products);
        }
    }
}
