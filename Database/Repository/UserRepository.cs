using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ToDoApp.Model;

namespace ToDoApp.Database.Repository
{
    public interface IUserRepository : IRepository<User>
    {
    }
    public class UserRepository : IUserRepository
    {
        private readonly ToDoAppDatabaseContext _db;

        public UserRepository(ToDoAppDatabaseContext db)
        {
            _db = db;
        }
        public int Count()
        {
            return _db.User.Count();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            return _db.User.Where(expression).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return _db.User.Select(a => a);
        }

        public User GetById(int id)
        {
            return _db.User.FirstOrDefault(a => a.UserId == id);
        }

        public IQueryable<User> GetMany(Expression<Func<User, bool>> expression)
        {
            return _db.User.Where(expression);
        }

        public bool Insert(User obj)
        {
            try
            {
                _db.User.Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
