using final_project.Data.Entities;
using final_project.Helpers;

namespace final_project.ViewModels;

public class ReturnPaginatedOrdersViewModel
{
    public IEnumerable<Order> Orders { get; set; }

    public PaginationProperties PaginationProperties { get; set; }
}