using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Project12.Helpers;
using Project12.Models;
using Project12.Services;

namespace Project12.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _mobile = string.Empty;
        private bool _entryAsPassword;
        private string _passwordIconCode = string.Empty;
        private string _signUpMessage = string.Empty;
        private bool _signUpMessageVisible;
        private Color _signUpMessageColor = Colors.White;
        private DBMokup _db;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public string UserEmail
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public string UserPassword
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public string Mobile
        {
            get => _mobile;
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public bool EntryAsPassword
        {
            get => _entryAsPassword;
            set
            {
                if (_entryAsPassword != value)
                {
                    _entryAsPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PasswordIconCode
        {
            get => _passwordIconCode;
            set
            {
                if (_passwordIconCode != value)
                {
                    _passwordIconCode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SignUpMessage
        {
            get => _signUpMessage;
            set
            {
                if (_signUpMessage != value)
                {
                    _signUpMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SignUpMessageVisible
        {
            get => _signUpMessageVisible;
            set
            {
                if (_signUpMessageVisible != value)
                {
                    _signUpMessageVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SignUpMessageColor
        {
            get => _signUpMessageColor;
            set
            {
                if (_signUpMessageColor != value)
                {
                    _signUpMessageColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ShowPasswordCommand { get; }
        public ICommand SignUpCommand { get; }

        public SignUpViewModel()
        {
            _db = new DBMokup();
            EntryAsPassword = true;
            PasswordIconCode = FontHelper.CLOSED_EYE_ICON;
            SignUpMessageVisible = false;

            ShowPasswordCommand = new Command(TogglePasswordButton);
            SignUpCommand = new Command(SignUp, Validate);
        }

        private void TogglePasswordButton()
        {
            if (EntryAsPassword)
                PasswordIconCode = FontHelper.OPEN_EYE_ICON;
            else
                PasswordIconCode = FontHelper.CLOSED_EYE_ICON;

            EntryAsPassword = !EntryAsPassword;
        }

        private bool Validate()
        {
            var fnameOK = !string.IsNullOrEmpty(FirstName);
            var lnameOK = !string.IsNullOrEmpty(LastName);
            var emailOK = !string.IsNullOrEmpty(UserEmail) && UserEmail.Contains("@") && UserEmail.Contains(".");
            var passOK = !string.IsNullOrEmpty(UserPassword) && UserPassword.Length > 5;
            var mobileOK = !string.IsNullOrEmpty(Mobile) && Mobile.Length == 10;

            return fnameOK && lnameOK && emailOK && passOK && mobileOK;
        }

        private async void SignUp()
        {
            SignUpMessageVisible = true;

            if (_db.IsEmailExist(UserEmail))
            {
                SignUpMessage = AppMessages.SignUpMessageFail;
                SignUpMessageColor = Colors.Red;
            }
            else
            {
                AppUser newUser = new AppUser
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    UserEmail = UserEmail,
                    UserPassword = UserPassword,
                    Mobile = Mobile
                };

                _db.AddUser(newUser);

                SignUpMessage = AppMessages.SignUpMessageSuccess;
                SignUpMessageColor = Colors.Green;

                await Toast.Make("SignUp new user", ToastDuration.Short, 14).Show();
            }
        }
    }
}