using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Database.Repository;
using ToDoApp.Model;
using ToDoApp.Service;

namespace ToDoApp
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IToDoListItemRepository, ToDoListItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
        }
    }
}
