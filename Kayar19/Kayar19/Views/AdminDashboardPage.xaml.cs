using Kayar19.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDashboardPage : ContentPage
    {
        public AdminDashboardPage()
        {

            InitializeComponent();
            GetUsers();
            GetUsersCount();
            GetAssetsCount();

        }

        public ObservableCollection<AddedUsers> Users { get; set; }

        public async void GetUsers()

        {
            HttpClient client = new HttpClient();
            var dashboardEndpoint = Helper.GetUsersurl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(dashboardEndpoint);
            var UsersList = JsonConvert.DeserializeObject<AddedUsers>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            UseList.ItemsSource = UsersList.data;

        }

        async void RecentUserTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedUser = e.Item as Models.Data;
            await Shell.Current.Navigation.PushModalAsync(new EditUserPage(selectedUser.Staff_Id));

        }
        private void SignOutClicked(object sender, EventArgs e)
        {
                ContentPage fpm = new LoginPage();
                HelperAppSettings.firstname = "";
                HelperAppSettings.lastname = "";
                HelperAppSettings.Token = "";
                HelperAppSettings.username = "";
                Application.Current.MainPage = fpm;
        }
        public async void GetUsersCount()
        {
                HttpClient client = new HttpClient();
            var UserCountEndpoint = Helper.userCounturl;
            client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var result = await client.GetStringAsync(UserCountEndpoint);
                var UsersCnt = JsonConvert.DeserializeObject<UserCounter>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);
            var hut = UsersCnt.data;
            totalUsersCount.BindingContext = UsersCnt.data[0];
        }

        public async void GetAssetsCount()
        {
            HttpClient client = new HttpClient();
            var AssetCountEndpoint = Helper.assetCounturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(AssetCountEndpoint);
            var AssetsCnt = JsonConvert.DeserializeObject<AssetCounter>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            TotalAssets.BindingContext = AssetsCnt.data[0];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUsers();
            GetUsersCount();
            GetAssetsCount();
        }
    }
}