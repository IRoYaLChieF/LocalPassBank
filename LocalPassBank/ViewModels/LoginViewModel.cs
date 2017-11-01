using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace LocalPassBank.ViewModels
{
    public class LoginViewModel
    {
        private ICommand goToRegistrationView = new DelegateCommand(() => { App.Current.MainWindow.DataContext = new RegistrationViewModel(); });
        public ICommand GoToRegistrationView
        {
            get { return (goToRegistrationView); }
        }
    }
}
