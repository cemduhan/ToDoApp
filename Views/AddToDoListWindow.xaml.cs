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
    /// Interaction logic for AddToDoListWindow.xaml
    /// </summary>
    public partial class AddToDoListWindow : Window
    {
        private ToDoListRepository _toDoListRepository = new ToDoListRepository(new ToDoAppDatabaseContext());
        private MainWindow _main;
        User _user;
        public AddToDoListWindow(User user, MainWindow main)
        {
            _user = user;
            _main = main;
            InitializeComponent();
        }

        private void listConfirm_Click(object sender, RoutedEventArgs e)
        {
            ToDoList _newList = new ToDoList();

            _newList.created_at = DateTime.Now;
            _newList.ToDoListUserId = _user.UserId;
            _newList.name = listName.Text;

            _toDoListRepository.Insert(_newList);
            _toDoListRepository.Save();

            _main.Populate();
            _main.RefreshList();

            this.Close();
        }
    }
}
