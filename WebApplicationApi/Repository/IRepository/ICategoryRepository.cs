using WebApplicationApi.Models;
namespace WebApplicationApi.Repository.IRepository
{
    // Interfaz que define los métodos para interactuar con la entidad Category en la base de datos
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category? GetCategory(int id);
        bool CategoryExists(string name);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
