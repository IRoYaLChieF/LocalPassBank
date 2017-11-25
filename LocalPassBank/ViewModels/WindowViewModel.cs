using LocalPassBank.Models;
using LocalPassBank.Views;
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
        public Page LoginPage => (loginPage);

        private Page registrationPage;
        public Page RegistrationPage => (registrationPage);

        private Page passBankPage;
        public Page PassBankPage => (passBankPage);

        private Database database;
        public Database Database => (database);

        private MainWindow window;

        public WindowViewModel(MainWindow window)
        {
            loginPage = new LoginPage(this);
            registrationPage = new RegistrationPage(this);
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

        public void GoToNewPassBankPage(int id, Byte[] key)
        {
            passBankPage = new PassBankPage(this, id, key);
            window.Navigate(passBankPage);
        }
    }
}
