using DiaryWPF.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models
{
    public class UserSettings : IDataErrorInfo
    {
        private bool _isServerNameValid;
        private bool _isDatabaseNameValid;
        private bool _isUserNameValid;
        private bool _isPasswordValid;
        public string ServerName
        {
            get => Settings.Default.ServerName;
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public string DatabaseName
        {
            get
            {
                return Settings.Default.DatabaseName;
            }
            set
            {
                Settings.Default.DatabaseName = value;
            }
        }
        public string UserName
        {
            get
            {
                return Settings.Default.UserName;
            }
            set
            {
                Settings.Default.UserName = value;
            }
        }
        public string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }
        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Pole Nazwa Serwera jest wymagane.";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            _isServerNameValid = true;
                        }                        
                        break;
                    case nameof(DatabaseName):
                        if (string.IsNullOrWhiteSpace(DatabaseName))
                        {
                            Error = "Pole Nazwa Serwera jest wymagane.";
                            _isDatabaseNameValid = false;
                        }
                        else
                        {
                            _isDatabaseNameValid = true;
                        }
                        break;
                    case nameof(UserName):
                        if (string.IsNullOrWhiteSpace(UserName))
                        {
                            Error = "Pole Nazwa Serwera jest wymagane.";
                            _isUserNameValid = false;
                        }
                        else
                        {
                            _isUserNameValid = true;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Pole Nazwa Serwera jest wymagane.";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            _isPasswordValid = true;
                        }
                        break;
                    default:
                        break;

                }
                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isDatabaseNameValid && _isServerNameValid && _isUserNameValid && _isPasswordValid;
            }
        }

        public void Save()
        {
            Settings.Default.Save();
        }


    }

}
