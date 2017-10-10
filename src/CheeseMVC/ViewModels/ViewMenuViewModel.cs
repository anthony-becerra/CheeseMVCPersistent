using CheeseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    // Part 3: Setting Up a Many-to-Many Relationship
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }
    }
}
