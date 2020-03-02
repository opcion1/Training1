using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Models.ViewModels
{
    public class MealFoodViewModel
    {
        public int MealId { get; set; }
        public int SesshinId { get; set; }
        public Food Food { get; set; }
    }
}
