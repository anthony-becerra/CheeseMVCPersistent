using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    // Part 1 - Setting Up the New Model
    public class CheeseCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<Cheese> Cheeses { get; set; }
    }
}
