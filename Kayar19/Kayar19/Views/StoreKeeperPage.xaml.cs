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
    public partial class StoreKeeperPage : ContentPage
    {
        public StoreKeeperPage()
        {
            InitializeComponent();
            GetSTKAssetsCount();
            GetAssignedCount();
            GetRequestCount();
            GetAllAssets();
        }
        public async void GetSTKAssetsCount()
        {
            HttpClient client = new HttpClient();
            var AssetCountEndpoint = Helper.assetCounturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(AssetCountEndpoint);
            var AssetsStkCnt = JsonConvert.DeserializeObject<AssetCounter>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            STKTotalAssets.BindingContext = AssetsStkCnt.data[0];
        }
        public async void GetAllAssets()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            HttpClient client = new HttpClient();
            var AssetEndpoint = Helper.GetAllAsseturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(AssetEndpoint);
            var NwAssetsList = JsonConvert.DeserializeObject<GetAssetsModel>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

           NewAssetList.ItemsSource = NwAssetsList.items;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        async void RecentAssetTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as Asset;
            await Shell.Current.Navigation.PushModalAsync(new EditAsset(selectedItem.item_id));

        }

        public async void GetRequestCount()
        {
            HttpClient client = new HttpClient();
            var requestCountEndpoint = Helper.requestCounturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(requestCountEndpoint);
            var requestStkCnt = JsonConvert.DeserializeObject<RequestCount>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            requestCount.BindingContext = requestStkCnt.data[0];
        }

        public async void GetAssignedCount()
        {
            HttpClient client = new HttpClient();
            var requestCountEndpoint = Helper.STKAssignedCounturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(requestCountEndpoint);
            var assignedStkCnt = JsonConvert.DeserializeObject<AssignedCounter>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            STKAssignedAsset.BindingContext = assignedStkCnt.data[0];
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetAllAssets();
            GetSTKAssetsCount();
            GetAssignedCount();
            GetRequestCount();

        }
    }
}