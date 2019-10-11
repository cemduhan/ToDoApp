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

namespace ToDoApp.Views
{
    /// <summary>
    /// Interaction logic for ToDoListItemWindow.xaml
    /// </summary>
    public partial class ToDoListItemWindow : Window
    {
        private ToDoListRepository _toDoListRepository = new ToDoListRepository(new ToDoAppDatabaseContext());
        private ToDoListItemRepository _toDoListItemRepository = new ToDoListItemRepository(new ToDoAppDatabaseContext());
        private AddToDoListItemWindow _childWindow;
        List<ToDoListItem> _toDoListItems;
        int _listId;
        public ToDoListItemWindow(int listId)
        {
            _listId = listId;
            InitializeComponent();

            filterStatus.IsChecked = true;
            filterName.IsChecked = false;
            Populate();
            RefreshList();
        }

        public void RefreshList()
        {            
            lbTodoListItem.ItemsSource = _toDoListItems;
        }

        public void SetExpired()
        {
            List<ToDoListItem> _list = _toDoListItemRepository.GetMany(a=> a.deadline < DateTime.Now).ToList();

            foreach(ToDoListItem _ in _list)
            {
                _.status = "Expired";

                _toDoListItemRepository.Delete(_.ToDoListItemId);

                _toDoListItemRepository.Save();

                _toDoListItemRepository.Insert(_);

                _toDoListItemRepository.Save();
            }

        }

        public void Populate(string sort="name")
        {
            SetExpired();

            switch (sort)
            {
                case "name":
                    _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).OrderBy(a=> a.name).ToList();
                    break;
                case "create_date":
                    _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).OrderBy(a => a.created_at).ToList();
                    break;
                case "deadline":
                    _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).OrderBy(a => a.deadline).ToList();
                    break;
                case "status":
                    _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).OrderBy(a => a.status).ToList();
                    break;
                default:
                    _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).ToList();
                    break;
            }            
        }

        private void listitemCreate_Click(object sender, RoutedEventArgs e)
        {
            // Create the window if it's not already created
            if (_childWindow == null)
                _childWindow = new AddToDoListItemWindow(_listId, this);

            // Show the window
            _childWindow.Show();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            int _itemId = 0;
            if (lbTodoListItem.SelectedItem != null)
            {
                _itemId = (lbTodoListItem.SelectedItem as ToDoListItem).ToDoListItemId;
            }
            else
            {
                return;
            }

            _toDoListItemRepository.Delete(_itemId);

            _toDoListItemRepository.Save();

            Populate();
            RefreshList();
        }

        private void MenuItemMarkDone_Click(object sender, RoutedEventArgs e)
        {
            int _itemId = 0;
            if (lbTodoListItem.SelectedItem != null)
            {
                _itemId = (lbTodoListItem.SelectedItem as ToDoListItem).ToDoListItemId;
            }
            else
            {
                return;
            }

            ToDoListItem _ = _toDoListItemRepository.Get(a => a.ToDoListItemId == _itemId);

            _.status = "Done";

            _toDoListItemRepository.Delete(_itemId);

            _toDoListItemRepository.Save();

            _toDoListItemRepository.Insert(_);

            _toDoListItemRepository.Save();

            Populate();
            RefreshList();
        }
        private void MenuItemSortName_Click(object sender, RoutedEventArgs e)
        {
            Populate();
            RefreshList();
        }
        private void MenuItemSortDeadline_Click(object sender, RoutedEventArgs e)
        {
            Populate("deadline");
            RefreshList();
        }
        private void MenuItemSortStatus_Click(object sender, RoutedEventArgs e)
        {
            Populate("status");
            RefreshList();
        }
        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter(FilterBox.Text);
        }

        public void ApplyFilter(string noFilter = "")
        {
            try
            {            
                if(noFilter != "" && noFilter != "Filter")
                {
                    if(filterStatus.IsChecked == true)
                    {
                        _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).Where(a => a.status.Contains(noFilter)).ToList();
                    
                    }
                    else
                    {
                        _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).Where(a => a.name.Contains(noFilter)).ToList();
                    
                    }
                    if(filterExpired.Content == "ShowExpiredOnly")
                    {
                        filterExpired.Content = "ShowAll";

                        _toDoListItems = _toDoListItems.Where(a => a.deadline < DateTime.Now).ToList();
                    }
                    else
                    {
                        filterExpired.Content = "ShowExpiredOnly";
                    }
                }
                else
                {
                    if (filterExpired.Content == "ShowExpiredOnly")
                    {
                        filterExpired.Content = "ShowAll";

                        _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).ToList();
                        _toDoListItems = _toDoListItems.Where(a => a.deadline < DateTime.Now).ToList();
                    }
                    else
                    {
                        filterExpired.Content = "ShowExpiredOnly";
                        _toDoListItems = _toDoListItemRepository.GetMany(a => a.ToDoListId == _listId).ToList();
                    }
                }                
            }
            catch
            {
                Populate();                
            }
            RefreshList();
        }

        private void filterExpired_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(FilterBox.Text);
        }

        private void filterStatus_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (filterStatus.IsChecked == true)
                {
                    filterName.IsChecked = false;
                }
                else
                {
                    filterName.IsChecked = true;
                }
            }
            catch
            {

            }
            

        }

        private void filterName_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (filterName.IsChecked == true)
                {
                    filterStatus.IsChecked = false;
                }
                else
                {
                    filterStatus.IsChecked = true;
                }
            }
            catch
            {

            }            
        }
    }
}
