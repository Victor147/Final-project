using final_project.Helpers;

namespace final_project.ViewModels;

public class ReturnPaginatedManufacturersViewModel
{
    public IEnumerable<ManufacturerViewModel> Manufacturers { get; set; }

    public PaginationProperties PaginationProperties { get; set; }
}