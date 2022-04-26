using DiaryWPF.Commands;
using DiaryWPF.Models;
using DiaryWPF.Models.Domains;
using DiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private void Confirm(object obj)
        {
            throw new NotImplementedException();
        }

        private void Close(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
