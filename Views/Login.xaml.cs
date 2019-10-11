using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ToDoApp.Database;
using ToDoApp.Database.Repository;
using ToDoApp.Model;
using ToDoApp.Service;

namespace ToDoApp
{
    /// <summary>
    /// Login.xaml etkileşim mantığı
    /// </summary>
    public partial class Login : Page
    {
        private UserRepository _userRepository = new UserRepository(new ToDoAppDatabaseContext());

        private PasswordOperation pass = new PasswordOperation();
        public Login()
        {
            InitializeComponent();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Regex.Unescape(UsernameLoginTextBox.Text);
            string password = Regex.Unescape(PasswordLoginTextBox.Password);

            if (username == "" || password == "")
            {
                //Invalid Values
                WrongInput.Content = "Invalid Input";
                return;
            }

            if (_userRepository.Get(a => a.username == username) == null)
            {
                //no user create one
                User _new = new User();

                _new.hashedpassword = pass.CreateHash(password);

                _new.username = username;

                _userRepository.Insert(_new);
                _userRepository.Save();

                MainWindow _main = new MainWindow(username);
                _main.Show();

                App.Current.Windows[0].Close();

            }
            else
            {
                //user found check password

                if(pass.CheckHash(password, _userRepository.Get(a => a.username == username).hashedpassword))
                {
                    MainWindow _main = new MainWindow(username);
                    _main.Show();

                    App.Current.Windows[0].Close();

                    return;
                }
                else
                {
                    //Incorrect Password
                    WrongInput.Content = "Wrong Username or Password";
                    return;
                }
            }
        }
    }
}
