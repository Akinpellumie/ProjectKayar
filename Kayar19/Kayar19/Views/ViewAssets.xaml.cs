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
    public partial class ViewAssets : ContentPage
    {
        public ViewAssets()
        {

            InitializeComponent();
            GetAllItems();

        }

        public ObservableCollection<GetAssetsModel> Items { get; set; }

        public async void GetAllItems()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            HttpClient client = new HttpClient();
            var AssetEndpoint = Helper.GetAllAsseturl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(AssetEndpoint);
            var ItemsList = JsonConvert.DeserializeObject<GetAssetsModel>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            AssetList.ItemsSource = ItemsList.items;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        async void ViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as Asset;
            await Shell.Current.Navigation.PushModalAsync(new EditAsset(selectedItem.item_id));

        }

        public async void SearchAssetBar_TextChanged(object sender, TextChangedEventArgs e)
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
                var AssetsList = JsonConvert.DeserializeObject<GetAssetsModel>(result);


                if (AssetsList != null)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;

                    //Itemsearch.IsVisible = true;
                    //BindingContext = ItemsList;
                    AssetList.ItemsSource = AssetsList.items;
                    //Itemsearch.ItemsSource = ItemsList;
                    // Autocomplete.IsEnabled = true;
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //acindicator.IsRunning = true;
                //acindicator.IsVisible = true;
                GetAllItems();
                //acindicator.IsRunning = false;
                //acindicator.IsVisible = false;


            }
        }
        async void AddAsset_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssetPage());
        }
        async void AssignAsset_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssignedAssetTab());
        }

        async void BatchAsset_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssetByBatch());
        }
        protected void PageRefreshing(object sender, EventArgs e)
        {
            GetAllItems();
            AssetList.EndRefresh();
        }
    }
}