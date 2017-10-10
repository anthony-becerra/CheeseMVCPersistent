using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CheeseCategory Category { get; set; }
        public int CategoryID { get; set; }
        public int ID { get; set; }
        // Part 3: Setting Up a Many-to-Many Relationship
        public IList<CheeseMenu> CheeseMenus { get; set; }
    }
}
