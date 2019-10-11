using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;

namespace ToDoApp.Database
{
    public class ToDoAppDatabaseContext : DbContext
    {
        public ToDoAppDatabaseContext()
        : base("name=ToDoAppDatabase")
        {
        }
        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ToDoListItem> ToDoListItem { get; set; }

    }
}
