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
    public interface IToDoListRepository : IRepository<ToDoList>
    {
    }
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoAppDatabaseContext _db;

        public ToDoListRepository(ToDoAppDatabaseContext db)
        {
            _db = db;
        }
        public int Count()
        {
            return _db.ToDoList.Count();
        }

        public bool Delete(int id)
        {
            try
            {
                ToDoList _obj = _db.ToDoList.FirstOrDefault(us => us.ToDoListId == id);

                if (_obj != null)
                {
                    _db.ToDoList.Remove(_obj);

                }
                else
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ToDoList Get(Expression<Func<ToDoList, bool>> expression)
        {
            return _db.ToDoList.FirstOrDefault(expression);
        }

        public IEnumerable<ToDoList> GetAll()
        {
            return _db.ToDoList.Select(a => a);
        }

        public ToDoList GetById(int id)
        {
            return _db.ToDoList.FirstOrDefault(a => a.ToDoListId == id);
        }

        public IQueryable<ToDoList> GetMany(Expression<Func<ToDoList, bool>> expression)
        {
            return _db.ToDoList.Where(expression);
        }

        public bool Insert(ToDoList obj)
        {
            try
            {
                _db.ToDoList.Add(obj);
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

        public bool Update(ToDoList obj)
        {
            throw new NotImplementedException();
        }
    }
}
