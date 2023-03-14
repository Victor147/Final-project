using final_project.Helpers;

namespace final_project.ViewModels;

public class ReturnPaginatedProductsViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; }

    public PaginationProperties PaginationProperties { get; set; }
}