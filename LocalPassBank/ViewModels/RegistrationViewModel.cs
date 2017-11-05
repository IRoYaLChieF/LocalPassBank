using LocalPassBank.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LocalPassBank.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private WindowViewModel windowViewModel;
        private DelegateCommand goToLoginPage;
        private DelegateCommand register;
        private PasswordBox passwordInput;
        private PasswordBox rePasswordInput;

        private String nameInput = String.Empty;
        public String NameInput
        {
            get { return (nameInput); }
            set
            {
                nameInput = value;
                OnPropertyChanged("NameInput");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RegistrationViewModel(WindowViewModel windowViewModel, PasswordBox passwordInput, PasswordBox rePasswordInput)
        {
            this.windowViewModel = windowViewModel;
            this.passwordInput = passwordInput;
            this.rePasswordInput = rePasswordInput;
            goToLoginPage = new DelegateCommand(() => this.windowViewModel.GoToLoginPage());
            register = new DelegateCommand(RegisterCommand, RegisterCanExecute);
            this.passwordInput.PasswordChanged += PasswordChanged;
            this.rePasswordInput.PasswordChanged += PasswordChanged;
        }

        public void ClearAllInput()
        {
            NameInput = String.Empty;
            passwordInput.Password = String.Empty;
            rePasswordInput.Password = String.Empty;
        }

        private void RegisterCommand()
        {
            Accounts account = windowViewModel.Database.GetAccountByName(nameInput);
            if (account != null)
            {
                MessageBox.Show("Ce nom de compte est déjà utilisé !", "Inscription" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            account = new Accounts(nameInput, Security.ComputeHash512(passwordInput.Password));
            windowViewModel.Database.AddAccount(account);
            MessageBox.Show("Inscription réussie !", "Inscription", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearAllInput();
            windowViewModel.GoToLoginPage();
        }

        private bool RegisterCanExecute()
        {
            if (nameInput == String.Empty || passwordInput.Password == String.Empty || rePasswordInput.Password == String.Empty ||
                passwordInput.Password != rePasswordInput.Password)
                return (false);
            return (true);
        }

        public ICommand Register
        {
            get { return (register); }
        }

        public ICommand GoToLoginPage
        {
            get { return (goToLoginPage); }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            register.RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(String info)
        {
            register.RaiseCanExecuteChanged();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
