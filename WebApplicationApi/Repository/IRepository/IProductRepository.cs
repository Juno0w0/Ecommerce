using WebApplicationApi.Models;
using WebApplicationApi.Repository.IRepository;
namespace WebApplicationApi.Repository.IRepository
{
    public interface IProductRepository 
    {
        ICollection<Product> GetProducts();
        ICollection<Product> GetProductsForCategory(int categoryId);
        ICollection<Product> SearchProduct(string name);
        Product? GetProduct(int id);
        bool BuyProduct(string name, int quantity);
        bool ProductExist(int id);
        bool ProductExist(string name);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();


    }
}
