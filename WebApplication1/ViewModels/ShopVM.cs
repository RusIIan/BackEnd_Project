using WebApplication1.Helper.Pagination;
using WebApplication1.Models;

namespace WebApplication1.ViewModels;

public class ShopVM
{
    public List<Product> Products { get; set; }
    public Pagination<Product> Pagination { get; set; }
    public List<Category> Categories { get; set; }
    public List<ProductColor> Colors { get; set; }
    public Baner Baner { get; set; }
}
