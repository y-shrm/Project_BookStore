using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;

namespace Project_BookStore.DataAccess.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public void Update(ShoppingCart obj);
        public void Save();
        

    }
}
