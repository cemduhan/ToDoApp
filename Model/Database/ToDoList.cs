using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Model
{
    public class ToDoList
    {
        public int ToDoListId { set; get; }
        public string name { set; get; }
        public int ToDoListUserId { set; get; }
        public DateTime created_at { set; get; }
    }
}
