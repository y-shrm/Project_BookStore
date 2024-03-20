using Project_BookStore.DataAccess.Data;
using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;
using System.Linq.Expressions;

namespace Project_BookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _db;
        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
