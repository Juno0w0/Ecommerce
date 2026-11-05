using WebApplicationApi.Repository.IRepository;
using WebApplicationApi.Data;
using WebApplicationApi.Models;
namespace WebApplicationApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CategoryExists(string name)
        {
            return _db.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }
        public bool CategoryExists(int id)
        {
            return _db.Categories.Any(c => c.Id == id);
        }
        public bool CreateCategory(Category category)
        {
            category.CreationDate = DateTime.UtcNow;
            _db.Categories.Add(category);
            return Save();
        }
        public bool DeleteCategory(Category category)
        {
            _db.Categories.Remove(category);
            return Save();
        }
        public Category? GetCategory(int id)
        {
            return _db.Categories.FirstOrDefault(c => c.Id == id);
        }
        public ICollection<Category> GetCategories()
        {
            return _db.Categories.OrderBy(c => c.Name).ToList();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
        public bool UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            return Save();
        }
    }
}
