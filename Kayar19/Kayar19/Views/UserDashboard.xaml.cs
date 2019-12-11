using Kayar19.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDashboard : ContentPage
    {
        public UserDashboard()
        {
            InitializeComponent();
            GetUserReqCount();
            GetAssignedAssets();
            GetAssetsUser();
            GetAssigned();
            GetUnreadMsgs();
        }
        async void SignOutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        public async void GetUserReqCount()
        {
            HttpClient client = new HttpClient();
            var ReqAssCountEndpoint = Helper.userRequestCounturl + HelperAppSettings.username;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(ReqAssCountEndpoint);
            var reqAssCnt = JsonConvert.DeserializeObject<StaffCounter>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            ReqAssetCnt.BindingContext = reqAssCnt.data[0];
        }
        public async void GetAssigned()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            HttpClient client = new HttpClient();
            var AssetEndpoint = Helper.StaffAssetCounturl + HelperAppSettings.username;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(AssetEndpoint);
            var NwAssignedList = JsonConvert.DeserializeObject<UserAssignedAsset>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            RecentAssigned.ItemsSource = NwAssignedList.items;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        async void RecentAssignedTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as Asset;
            await Shell.Current.Navigation.PushModalAsync(new EditAsset(selectedItem.item_id));

        }

        public async void GetAssetsUser()
        {
            HttpClient client = new HttpClient();
            var requestCountEndpoint = Helper.StaffAssetCounturl + HelperAppSettings.username;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(requestCountEndpoint);
            var AssignedCnt = JsonConvert.DeserializeObject<UserAssignedAsset>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            AssetsUser.BindingContext = AssignedCnt;
        }

        public async void GetAssignedAssets()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            HttpClient client = new HttpClient();
            var AssetEndpoint = Helper.UserAssignedurl + HelperAppSettings.username;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(AssetEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<GetAssetsModel>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            RecentAssigned.ItemsSource = ItemsList.items;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUserReqCount();
            GetAssignedAssets();
            GetAssetsUser();
            GetAssigned();
            GetUnreadMsgs();
        }
        public async void MsgClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserNotification());
        }
        public async void GetUnreadMsgs()

        {
            HttpClient client = new HttpClient();
            var UnreadEndpoint = Helper.UnreadNotificationurl + HelperAppSettings.username + Helper.UnreadCounturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UnreadEndpoint);
            var MessageList = JsonConvert.DeserializeObject<MessageCountModels>(result);

            UnreadMsg.BindingContext = MessageList.count[0];
        }
    }
}