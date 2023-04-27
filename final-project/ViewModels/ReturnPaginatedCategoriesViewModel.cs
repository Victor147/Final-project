using final_project.Helpers;

namespace final_project.ViewModels;

public class ReturnPaginatedCategoriesViewModel
{
    public IEnumerable<CategoryViewModel> Categories { get; set; }

    public PaginationProperties PaginationProperties { get; set; }
}