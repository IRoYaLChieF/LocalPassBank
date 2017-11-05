using LocalPassBank.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LocalPassBank.ViewModels
{
    public class WindowViewModel
    {
        private Page loginPage;
        public Page LoginPage
        {
            get { return (loginPage); }
        }

        private Page registrationPage;
        public Page RegistrationPage
        {
            get { return (registrationPage); }
        }

        private Database database;
        public Database Database
        {
            get { return(database); }
        }

        private MainWindow window;

        public WindowViewModel(MainWindow window)
        {
            loginPage = new Views.LoginPage(this);
            registrationPage = new Views.RegistrationPage(this);
            database = new Database();
            this.window = window;
        }

        public void GoToLoginPage()
        {
            window.Navigate(loginPage);
        }

        public void GoToRegistrationPage()
        {
            window.Navigate(registrationPage);
        }
    }
}
