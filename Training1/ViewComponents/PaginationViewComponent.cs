using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Training1.Models.ViewModels;

namespace Training1.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(
            int numberOfItems, int itemsPerPage, int currentPage, string controller, string action, string category)
        {
            PaginationViewModel vm = new PaginationViewModel
            {
                NumberOfItems = numberOfItems,
                ItemsPerPage = itemsPerPage,
                CurrentPage = currentPage,
                NumberOfPage = (int)Math.Ceiling(numberOfItems / (double)itemsPerPage),
                ControllerName = controller,
                ActionName = action,
                CategoryFilter = category
            };
            return View(vm);
        }
    }
}
