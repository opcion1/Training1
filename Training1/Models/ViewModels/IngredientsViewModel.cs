using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Models.ViewModels
{
    public class IngredientsViewModel
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public int FoodId { get; set; }
        public int MealId { get; set; }
    }
}
