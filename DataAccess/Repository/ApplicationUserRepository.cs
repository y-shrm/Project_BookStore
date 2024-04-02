using Project_BookStore.DataAccess.Data;
using Project_BookStore.DataAccess.IRepository;
using Project_BookStore.Models;
using System.Linq.Expressions;

namespace Project_BookStore.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private AppDbContext _db;
        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        
    }
}
