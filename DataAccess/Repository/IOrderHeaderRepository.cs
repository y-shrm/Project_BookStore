using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;

namespace Project_BookStore.DataAccess.Repository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        public void Update(OrderHeader obj);
        public void Save();
    }
}
