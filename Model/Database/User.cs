using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Model
{
    public class User
    {
        public int UserId { set; get; }
        public string username { set; get; }
        public string hashedpassword { set; get; }
    }
}
