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
    public partial class MakeRequestTabbed : TabbedPage
    { public ObservableCollection<Models.Asset> item_id { get; set; }
        
        public ObservableCollection<Models.Asset> Items { get; private set; }
        public MakeRequestTabbed()
        {
            InitializeComponent();
            //GetAddedItems();
        }

        private async void arrowClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AddItems());
            //await Shell.Current.GoToAsync("//ItemsPage");
            await Shell.Current.Navigation.PopModalAsync();
        }

        public void GetAddedItems()
        {

            var addedAssets = Helper.listAssetA;
            Items = new ObservableCollection<Models.Asset>(addedAssets);
            Emplist.ItemsSource = Items;
        }

     

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            var addedAssets = Helper.listAssetA;
            Items = new ObservableCollection<Models.Asset>(addedAssets);
            Emplist.ItemsSource = Items;


        }

        private void RemoveItem(Object Sender, EventArgs args)
        {
            ImageButton button = (ImageButton)Sender;
            var Selecteditem = button.CommandParameter as Asset;
            var SelectedId = Selecteditem.item_id;
            Helper.listAssetA.RemoveAll(x => x.item_id == SelectedId);
        }

        private async void RequestAssetClicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;

            RequestModel assetmodel = new RequestModel();
            Int32 length = Helper.listAssetA.Count;
            assetmodel.items = Helper.listAssetA;
            assetmodel.staffUsername = HelperAppSettings.username;
            assetmodel.comment = txtAssetComment.Text;


            string json = JsonConvert.SerializeObject(assetmodel, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.PostAsync(Helper.requestAsseturl, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Request completed Successfully", "Ok");
                Emplist.ItemsSource = null;
                Helper.listAssetA.Clear();
                assetmodel.comment = " ";
                indicator.IsRunning = false;
                indicator.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Error ", "Bad request, Try Again", "Ok");
            }

        }
    }
}