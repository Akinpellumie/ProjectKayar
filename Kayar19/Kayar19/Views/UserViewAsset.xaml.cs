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
    public partial class UserViewAsset : ContentPage
    {
        public UserViewAsset()
        {

            InitializeComponent();
            GetAvailableItems();

        }

        //public ObservableCollection<GetAssetsModel> Items { get; set; }

        public async void GetAvailableItems()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            HttpClient client = new HttpClient();
            var UAEndpoint = Helper.GetAllAsseturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UAEndpoint);
            var UALists = JsonConvert.DeserializeObject<GetAssetsModel>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            AllAssetList.ItemsSource = UALists.items;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        async void VAItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as Asset;
            await Shell.Current.Navigation.PushModalAsync(new EditAsset(selectedItem.item_id));

        }

        public async void UserSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 2)
            {


                if (Helper.GetAllAsseturl == null)
                {
                    indicator.IsRunning = true;
                    indicator.IsVisible = true;
                }

                //string url = $"http://192.168.1.118:5000/items?ItemName={e.NewTextValue}";
                var url = Helper.SearchAsseturl + e.NewTextValue;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var result = await client.GetStringAsync(url);
                var UsersList = JsonConvert.DeserializeObject<GetAssetsModel>(result);


                if (UsersList != null)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;

                    //Itemsearch.IsVisible = true;
                    //BindingContext = ItemsList;
                    AllAssetList.ItemsSource = UsersList.items;
                    //Itemsearch.ItemsSource = ItemsList;
                    // Autocomplete.IsEnabled = true;
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //acindicator.IsRunning = true;
                //acindicator.IsVisible = true;
                GetAvailableItems();
                //acindicator.IsRunning = false;
                //acindicator.IsVisible = false;


            }
        }
        async void AddAsset_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssetPage());
        }
        async void MakeRequest_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MakeRequestTabbed());
        }
        
        protected void PageRefreshing(object sender, EventArgs e)
        {
            GetAvailableItems();
            AllAssetList.EndRefresh();
        }
    }
}