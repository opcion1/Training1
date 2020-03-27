

namespace Training1.Models.ViewModels
{
    public class PaginationViewModel
    {
        public int NumberOfItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfPage { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public ProductCategory? CategoryFilter { get; set; }
    }
}
