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
    public partial class PreviewAssetAssign : ContentPage
    {
        //public ObservableCollection<Models.Asset> Item { get; private set; }
        public ObservableCollection<Models.Asset> Items { get; private set; }
        public ObservableCollection<Models.Data> Users { get; private set; }

        public PreviewAssetAssign()
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
            ToBeAssignedList.ItemsSource = Items;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var addedItems = ConstantsValue.listItemA;
        //    Items = new ObservableCollection<ItemsModel>(addedItems);
        //    Emplist.ItemsSource = Items;

        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var addedAssets = Helper.listAssetA;
            Items = new ObservableCollection<Models.Asset>(addedAssets);
            ToBeAssignedList.ItemsSource = Items;

            var addedUsers = Helper.listUserA;
            Users = new ObservableCollection<Models.Data>(addedUsers);
            AssignUser.ItemsSource = Users;


        }

        private void RemoveItem(Object Sender, EventArgs args)
        {
            ImageButton button = (ImageButton)Sender;
            var Selecteditem = button.CommandParameter as Asset;
            var SelectedId = Selecteditem.item_id;
            Helper.listAssetA.RemoveAll(x => x.item_id == SelectedId);
        }

        private async void AssignAssetClicked(object sender, EventArgs e)
        {

            indicator.IsRunning = true;
            indicator.IsVisible = true;

            AssignModel assetmodel = new AssignModel();
            Int32 length = Helper.listAssetA.Count;
            //var UserName = Helper.listUserA;
            assetmodel.assets = Helper.listAssetA;
            assetmodel.staff_username = Helper.listUserA[0].UserName;
            assetmodel.storeKeeperUsername= HelperAppSettings.username;
            assetmodel.location = txtAssLocation.Text;
            assetmodel.requestStatus = "Nill Request";

            string json = JsonConvert.SerializeObject(assetmodel, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.PostAsync(Helper.assignAsseturl, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Request completed Successfully", "Ok");
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                ToBeAssignedList.ItemsSource = null;
                Helper.listAssetA.Clear();
                Helper.listUserA.Clear();
                txtAssLocation.Text = "";

            }
            else
            {
                await DisplayAlert("Error ", "Bad request, Try Again", "Ok");
            }

        }
        public void GetAddedUsers()
        {

            var addedUsers = Helper.listUserA;
            Users = new ObservableCollection<Models.Data>(addedUsers);
            AssignUser.ItemsSource = Users;
        }



        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var addedAssets = Helper.listAssetA;
        //    Items = new ObservableCollection<Models.Asset>(addedAssets);
        //    Emplist.ItemsSource = Items;


        //}

        private void RemoveUser(Object Sender, EventArgs args)
        {
            ImageButton button = (ImageButton)Sender;
            var Selecteduser = button.CommandParameter as Models.Data;
            var SelectedId = Selecteduser.Staff_Id;
            Helper.listUserA.RemoveAll(x => x.Staff_Id == SelectedId);
        }

    }
}