using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    // Part 1 - Adding Categories
    public class CategoryController : Controller
    {

        //This object will be the mechanism with which we interact with objects stored in the database.
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<CheeseCategory> cheeseCategories = context.Categories.ToList();
            return View(cheeseCategories);
        }

        // Part 1 - ADD ACTION METHODS
        public IActionResult Add()
        {
            AddCategoryViewModel addCat = new AddCategoryViewModel();
            return View(addCat);
        }

        // Part 1 - ADD ACTION METHODS
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = obj.Name // assign value of Name from the ViewModel
                };
                context.Categories.Add(newCategory); // Add newCategory to the database context
                context.SaveChanges(); // Save changes to the database
                return Redirect("/Category");
            }

            return View(obj);
        }
    }
}
