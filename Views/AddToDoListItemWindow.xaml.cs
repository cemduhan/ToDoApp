using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoApp.Database;
using ToDoApp.Database.Repository;
using ToDoApp.Model;

namespace ToDoApp.Views
{
    /// <summary>
    /// Interaction logic for AddToDoListItemWindow.xaml
    /// </summary>
    public partial class AddToDoListItemWindow : Window
    {
        private int _listId;
        private ToDoListItemWindow _master;
        private ToDoListItemRepository _toDoListItemRepository = new ToDoListItemRepository(new ToDoAppDatabaseContext());
        public AddToDoListItemWindow(int listId, ToDoListItemWindow _list)
        {
            _listId = listId;
            _master = _list;
            InitializeComponent();
        }

        private void listConfirm_Click(object sender, RoutedEventArgs e)
        {
            ToDoListItem _newListItem = new ToDoListItem();

            _newListItem.created_at = DateTime.Now;
            _newListItem.status = itemStatus.Text;
            _newListItem.name = itemName.Text;
            _newListItem.explanation = itemDesc.Text;
            _newListItem.deadline = itemDeadline.SelectedDate ?? DateTime.Now;
            _newListItem.ToDoListId = _listId;

            _toDoListItemRepository.Insert(_newListItem);

            _toDoListItemRepository.Save();

            _master.Populate();
            _master.RefreshList();

            this.Close();
        }
    }
}
