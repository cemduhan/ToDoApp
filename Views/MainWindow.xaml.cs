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
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Shapes;
using ToDoApp.Database.Repository;
using ToDoApp.Service;
using System.Data;
using ToDoApp.Database;
using ToDoApp.Model;
using ToDoApp.Views;
using System.ComponentModel;

namespace ToDoApp
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {

        private UserRepository _userRepository = new UserRepository(new ToDoAppDatabaseContext());
        private ToDoListRepository _toDoListRepository = new ToDoListRepository(new ToDoAppDatabaseContext());
        private AddToDoListWindow _childWindow;
        private ToDoListItemWindow _detailedWindow;
        List<ToDoList> _toDoLists;
        User _usr;
        public MainWindow(string username)
        {
            _usr = _userRepository.Get(a => a.username == username);

            InitializeComponent();

            Populate();
            RefreshList();
        }

        public void Populate()
        {
            _toDoLists = _toDoListRepository.GetMany(a => a.ToDoListUserId == _usr.UserId).ToList();
        }
        public void RefreshList()
        {            
            lbTodoList.ItemsSource = _toDoLists;
        }
        
        private void listCreate_Click(object sender, RoutedEventArgs e)
        {
            // Create the window if it's not already created
            if (_childWindow == null)
                _childWindow = new AddToDoListWindow(_usr, this);

            // Show the window
            _childWindow.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Close the child window only when this window closes
            _childWindow.Close();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            int listId = 0 ;
            if (lbTodoList.SelectedItem != null)
            {
                listId = (lbTodoList.SelectedItem as ToDoList).ToDoListId;
            }                
            else
            {
                return;
            }

           _toDoListRepository.Delete(listId);

            _toDoListRepository.Save();

            Populate();
            RefreshList();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            int listId = 0;
            if (lbTodoList.SelectedItem != null)
            {
                listId = (lbTodoList.SelectedItem as ToDoList).ToDoListId;
                
                if (_detailedWindow == null)
                    _detailedWindow = new ToDoListItemWindow(listId);

                // Show the window
                _detailedWindow.Show();
            }
            else
            {
                return;
            }
        }
    }
}
