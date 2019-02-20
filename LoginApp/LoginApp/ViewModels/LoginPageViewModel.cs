using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LoginApp.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        Page _page;
        public LoginPageViewModel(Page page)
        {
            _page = page;
            LoginCommand = new Command(Login, () => allowLogin);
        }

        string email = string.Empty;
        string password = string.Empty;
        bool allowLogin = false;
        
        public string Email
        {
            get => email;
            set {
                email = value;
                if (!string.IsNullOrWhiteSpace(email) && validateEmail() && !string.IsNullOrWhiteSpace(password))
                {
                    AllowLogin = true;
                }
                else
                {
                    AllowLogin = false;
                }
            }
        }
        public string Password
        {
            get => password;
            set {
                password = value;
                if (!string.IsNullOrWhiteSpace(email) && validateEmail() && !string.IsNullOrWhiteSpace(password))
                {
                    AllowLogin = true;
                }
                else
                {
                    AllowLogin = false;
                }
            }
        }
        public bool AllowLogin
        {
            get => allowLogin;
            set
            {
                allowLogin = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        bool validateEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        public Command LoginCommand { get; }
        public async void Login()
        {
            await _page.DisplayAlert("Du är inloggad", $"Inloggad som: { email }", "OK");
        }
        
    }
}
