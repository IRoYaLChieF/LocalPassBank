using LocalPassBank.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LocalPassBank.ViewModels
{
    class PassBankViewModel : INotifyPropertyChanged
    {
        private WindowViewModel windowViewModel;
        private int id;
        private Byte[] key;

        private PasswordInfo[] passwordsInfo;
        public PasswordInfo[] PasswordsInfo
        {
            get => (passwordsInfo);
            set => passwordsInfo = value;
        }

        private DelegateCommand addPassword;
        public ICommand AddPassword => (addPassword);

        public event PropertyChangedEventHandler PropertyChanged;

        public PassBankViewModel(WindowViewModel windowViewModel, int id, Byte[] key)
        {
            this.windowViewModel = windowViewModel;
            this.id = id;
            this.key = key;
            PasswordsInfo = windowViewModel.Database.GetPasswordInfoFromId(id, key);
            addPassword = new DelegateCommand(AddPasswordCommand);
        }

        private void AddPasswordCommand()
        {
            windowViewModel.Database.AddPassword(id,
                Security.Encryption("LoL", key),
                Security.Encryption("League of Legends Account", key),
                Security.Encryption("sorpix", key),
                Security.Encryption("206ccc", key));
            List<PasswordInfo> list = new List<PasswordInfo>(passwordsInfo.AsEnumerable());
            list.Add(new PasswordInfo(Security.Encryption("LoL", key), Security.Encryption("League of Legends Account", key), key));
            passwordsInfo = list.ToArray();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PasswordsInfo"));
        }
    }
}
