using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Kayar19.Models;
using Kayar19.Services;
using Xamarin.Forms;

namespace Kayar19.ViewModels
{
    public class ViewUsersViewModel : INotifyPropertyChanged
    {
        private readonly ServiceAPI _apiServices = new ServiceAPI();
        private List<User> _users;

        //public string AccessToken { get; set; }
        public List<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public ICommand GetUsersCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var access_token = Token.access_token;
                    Users = await ServiceAPI.GetUsersAsync(access_token);
                });
            }
        }

        //public ICommand LogoutCommand
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {
        //            Settings.AccessToken = string.Empty;
        //            Debug.WriteLine(Settings.Username);
        //            Settings.Username = string.Empty;
        //            Debug.WriteLine(Settings.Password);
        //            Settings.Password = string.Empty;

        //            // navigate to LoginPage
        //        });
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
