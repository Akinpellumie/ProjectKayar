using Kayar19.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestPage2 : ContentPage
    {
        public RequestPage2()
        {
            InitializeComponent();
            GetAvailableItems();
        }
        public ObservableCollection<Models.Asset> Items { get; set; }

        public async void GetAvailableItems()

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

            ReqAssetList.ItemsSource = ItemsList.items;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        public async void RequestSearchBar_TextChanged(object sender, TextChangedEventArgs e)
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
                var ReqAssetsList = JsonConvert.DeserializeObject<GetAssetsModel>(result);


                if (ReqAssetsList != null)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;

                    //Itemsearch.IsVisible = true;
                    //BindingContext = ItemsList;
                    ReqAssetList.ItemsSource = ReqAssetsList.items;
                    //Itemsearch.ItemsSource = ItemsList;
                    // Autocomplete.IsEnabled = true;
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                GetAvailableItems();

            }
        }
        async void AddReqItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as Asset;
            await Shell.Current.Navigation.PushModalAsync(new EditAsset(selectedItem.item_id));

        }
      
        private async void arrowClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
        public void AddItemToComponent(Object Sender, EventArgs args)
        {
            ImageButton button = (ImageButton)Sender;

            //var button = sender as Button;
            var Selecteditem = button.CommandParameter as Asset;
            Helper.listAssetA.Add(Selecteditem);

            PopupNavigation.Instance.PushAsync(new AssetQtyPopUp());
            MessagingCenter.Subscribe<Models.Asset>(this, "PopUpData", (value) =>
            {

                var index = Helper.listAssetA.Count - 1;
                Helper.listAssetA[index].Quantity = value.Quantity;

                ////ConstantsValue.listItemA.Where(p => p.Quantity == obj.EmpNo).Update();
                ////Selecteditem.Quantity = value.Quantity;
                //if (Selecteditem.Quantity == "1")
                //{
                   
                //    var cat = Helper.listAssetA.FirstOrDefault(d => d.Quantity == "1");
                //    string receivedData = value.Quantity;
                //    if (cat != null) { cat.Quantity = value.Quantity; }
                //    DisplayAlert(Selecteditem.item_id, "Request Added", "OK");
                //}

            });

        }

    }
}