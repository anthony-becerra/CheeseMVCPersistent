using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            // call to retrieve all Cheese objects
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();
            ViewBag.title = "My Cheeses";
            return View(cheeses);
        }

        public IActionResult Add()
        {
            // Get collection of all cheese category objects and pass into constructor.
            List<CheeseCategory> categories = context.Categories.ToList();
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(categories);
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {

            if (ModelState.IsValid)
            {
                // Get CategoryID property from ViewModel passed in
                CheeseCategory newCheeseCategory =
                        context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            // If you try to add cheese without parameter, this fixes bug where categories disappear
            List<CheeseCategory> categories = context.Categories.ToList();
            AddCheeseViewModel addCheeseViewModel2 = new AddCheeseViewModel(categories);
            return View(addCheeseViewModel2);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }
    }
}
