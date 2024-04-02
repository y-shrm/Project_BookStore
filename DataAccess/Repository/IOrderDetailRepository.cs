using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;

namespace Project_BookStore.DataAccess.Repository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        public void Update(OrderDetail obj);
        public void Save();

    }
}
