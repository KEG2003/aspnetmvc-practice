using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // _context =  dependency injection we save expense model to _context to the database
        private readonly SpendSmartDbContext _context;


        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            //everytime we submit , we retrieve expenses from _context => Expenses table. and formed into a list for view 
            var allExpenses = _context.Expenses.ToList(); 
            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int? id)
        {
            return View();
        }
        public IActionResult CreateEditExpenseForm(Expense model)
        {

            _context.Expenses.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult Delete(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
