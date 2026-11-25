using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using Project12.Helpers;
using Project12.Services;

namespace Project12.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private bool _entryAsPassword;
        private string _passwordIconCode = string.Empty;
        private string _loginMessage = string.Empty;
        private bool _signInMessageVisible;
        private Color _signInMessageColor = Colors.White;
        private DBMokup _db;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsSignInButtonEnabled));
                    (SignInCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public string UserPassword
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsSignInButtonEnabled));
                    (SignInCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public bool IsSignInButtonEnabled
        {
            get => !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword));
        }

        public bool EntryAsPassword
        {
            get => _entryAsPassword;
            set
            {
                if (value != _entryAsPassword)
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
                if (value != _passwordIconCode)
                {
                    _passwordIconCode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LoginMessage
        {
            get => _loginMessage;
            set
            {
                if (_loginMessage != value)
                {
                    _loginMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SignInMessageVisible
        {
            get => _signInMessageVisible;
            set
            {
                if (value != _signInMessageVisible)
                {
                    _signInMessageVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SignInMessageColor
        {
            get => _signInMessageColor;
            set
            {
                if (value != _signInMessageColor)
                {
                    _signInMessageColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ShowPasswordCommand { get; }
        public ICommand SignInCommand { get; }

        public SignInViewModel()
        {
            _db = new DBMokup();
            EntryAsPassword = true;
            PasswordIconCode = FontHelper.OPEN_EYE_ICON;
            SignInMessageVisible = false;

            ShowPasswordCommand = new Command(TogglePasswordButton);
            SignInCommand = new Command(OnSignInButtonClick, () => IsSignInButtonEnabled);
        }

        private void TogglePasswordButton()
        {
            if (EntryAsPassword)
                PasswordIconCode = FontHelper.CLOSED_EYE_ICON;
            else
                PasswordIconCode = FontHelper.OPEN_EYE_ICON;

            EntryAsPassword = !EntryAsPassword;
        }

        private void OnSignInButtonClick()
        {
            SignInMessageVisible = true;
            if (_db.IsExist(UserName, UserPassword))
            {
                LoginMessage = AppMessages.SignInMessageSuccess;
                SignInMessageColor = Colors.Green;
                // Navigate to MainPage
            }
            else
            {
                LoginMessage = AppMessages.SignMessageFail;
                SignInMessageColor = Colors.Red;
            }
        }
    }
}
