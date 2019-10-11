using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Database;
using ToDoApp.Database.Repository;
using ToDoApp.Views;

namespace ToDoApp.Test
{
    [TestFixture]
    class UTest
    {
        string _usr = "username";
        int _listId = 1;
        ToDoListItemWindow _testItemWindow;
        MainWindow _testMainWindow;
        ToDoListItemRepository _toDoListItemRepository = new ToDoListItemRepository(new ToDoAppDatabaseContext());
        ToDoListRepository _toDoListRepository = new ToDoListRepository(new ToDoAppDatabaseContext());
        UserRepository _userRepository = new UserRepository(new ToDoAppDatabaseContext());


        [SetUp()]
        public void Init()
        {
            _testItemWindow = new ToDoListItemWindow(_listId);
            _testMainWindow = new MainWindow(_usr);

            _testItemWindow.Populate();
            _testMainWindow.Populate();
        }

        [Test]
        public void ChechkToDoListItemWindowCreation()
        {
            Assert.AreEqual(_testItemWindow._listId, _listId);
        }

        [Test]
        public void ChechkToDoListItemWindowPopulate()
        {
            //Assert if window can populate its list
            // if no record will give an error
            Assert.AreEqual(_toDoListItemRepository.GetMany(a=> a.ToDoListId == _listId).Count(), _testItemWindow._toDoListItems.Count());
        }

        [Test]
        public void ChechkToDoListWindowPopulate()
        {
            //Assert if window can populate its list
            // if no record will give an error

            _testMainWindow.Populate();

            Assert.AreEqual(_toDoListRepository.GetMany(a => a.ToDoListUserId == _userRepository.Get(b=> b.username == _usr).UserId).Count(), _testMainWindow._toDoLists.Count());
        }
    }
}
