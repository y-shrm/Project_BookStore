using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;

namespace Project_BookStore.DataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product obj);
        public void Save();
        

    }
}
