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
    public interface IToDoListItemRepository : IRepository<ToDoListItem>
    {
    }
    public class ToDoListItemRepository : IToDoListItemRepository
    {
        private readonly ToDoAppDatabaseContext _db;

        public ToDoListItemRepository(ToDoAppDatabaseContext db)
        {
            _db = db;
        }
        public int Count()
        {
            return _db.ToDoListItem.Count();
        }

        public bool Delete(int id)
        {
            try
            {
                ToDoListItem _obj = _db.ToDoListItem.FirstOrDefault(us => us.ToDoListItemId == id);

                if (_obj != null)
                {
                    _db.ToDoListItem.Remove(_obj);

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

        public ToDoListItem Get(Expression<Func<ToDoListItem, bool>> expression)
        {
            return _db.ToDoListItem.FirstOrDefault(expression);
        }

        public IEnumerable<ToDoListItem> GetAll()
        {
            return _db.ToDoListItem.Select(a => a);
        }

        public ToDoListItem GetById(int id)
        {
            return _db.ToDoListItem.FirstOrDefault(a => a.ToDoListItemId == id);
        }

        public IQueryable<ToDoListItem> GetMany(Expression<Func<ToDoListItem, bool>> expression)
        {
            return _db.ToDoListItem.Where(expression);
        }

        public bool Insert(ToDoListItem obj)
        {
            try
            {
                _db.ToDoListItem.Add(obj);
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

        public bool Update(ToDoListItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
