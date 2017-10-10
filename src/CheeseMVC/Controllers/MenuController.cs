using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

// Part 3: Setting Up a Many-to-Many Relationship
namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        //This object will be the mechanism with which we interact with objects stored in the database.
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            ViewBag.title = "Menus";
            return View(menus);
        }

        public IActionResult Add()
        {
            ViewBag.title = "New Menu";
            AddMenuViewModel menuViewModel = new AddMenuViewModel();
            return View(menuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = obj.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect($"/Menu/ViewMenu/{newMenu.ID}");
            }

            return View(obj);
        }

        public IActionResult ViewMenu(int id)
        {
            // retrieve the menu with the given ID
            Menu menu = context.Menus.Single(m => m.ID == id);

            // retrieve items associated with the menu
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewMenuViewModel);

        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            IEnumerable<Cheese> cheeses = context.Cheeses.ToList();

            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, cheeses);
            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {

            if (ModelState.IsValid)
            {
                int cheeseID = addMenuItemViewModel.CheeseID;
                int menuID = addMenuItemViewModel.MenuID;

                // look for a CheeseMenu object with the given IDs
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu cheeseMenu = new CheeseMenu()
                    {
                        MenuID = menuID,
                        CheeseID = cheeseID
                    };

                    context.CheeseMenus.Add(cheeseMenu);
                    context.SaveChanges();
                }

                return Redirect($"/Menu/ViewMenu/{menuID}");
            }

            return View(addMenuItemViewModel);
        }
    }
}
