using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;

namespace Project_BookStore.DataAccess.Repository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        public void Update(Category obj);
        public void Save();
        

    }
}
