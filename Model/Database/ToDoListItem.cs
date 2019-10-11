using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Model
{
    public class ToDoListItem
    {
        public int ToDoListItemId { set; get; }

        public int ToDoListId { set; get; }

        public string name { set; get; }

        public string explanation { set; get; }

        public string status { set; get; }

        public DateTime deadline { set; get; }

        public DateTime created_at { set; get; }
    }
}
