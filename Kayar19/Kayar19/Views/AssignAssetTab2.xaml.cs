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
    public partial class AssignAssetTab2 : ContentPage
    {
        public AssignAssetTab2()
        {

            InitializeComponent();
            GetUsers();

        }

        public ObservableCollection<AddedUsers> Users { get; set; }

        public async void GetUsers()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;


            HttpClient client = new HttpClient();
            var dashboardEndpoint = Helper.GetUsersurl;
            var imageEndpoint = Helper.Imageurl;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(dashboardEndpoint);
            var UsersList = JsonConvert.DeserializeObject<AddedUsers>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            Emplist.ItemsSource = UsersList.data;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        async void ViewUserTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedUser = e.Item as Models.Data;
            await Shell.Current.Navigation.PushAsync(new EditUserPage(selectedUser.Staff_Id));

        }
        public async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 2)
            {


                if (Helper.GetUsersurl == null)
                {
                    indicator.IsRunning = true;
                    indicator.IsVisible = true;
                    // Autocomplete.IsEnabled = false;
                }

                var url = Helper.SearchUsersurl + e.NewTextValue;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var result = await client.GetStringAsync(url);
                var UsersList = JsonConvert.DeserializeObject<AddedUsers>(result);


                if (UsersList != null)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;

                    //Itemsearch.IsVisible = true;
                    //BindingContext = ItemsList;
                    Emplist.ItemsSource = UsersList.data;
                    //Itemsearch.ItemsSource = ItemsList;
                    // Autocomplete.IsEnabled = true;
                }
            }

            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //acindicator.IsRunning = true;
                //acindicator.IsVisible = true;
                GetUsers();
                //acindicator.IsRunning = false;
                //acindicator.IsVisible = false;


            }
        }
        async void AddUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewUserPage());
        }
        protected void PageRefreshing(object sender, EventArgs e)
        {
            GetUsers();
            Emplist.EndRefresh();
        }
        public void PickUser_Clicked(Object Sender, EventArgs args)
        {
            ImageButton button = (ImageButton)Sender;


            var Selecteduser = button.CommandParameter as Models.Data;
            Helper.listUserA.Add(Selecteduser);
            //Helper.listUserA.Add(Selecteduser);
            //PopupNavigation.Instance.PushAsync(new AssetQtyPopUp());
            //MessagingCenter.Subscribe<Models.Data>(this, "PopUpData", (value) =>
            //{

            //    var index = Helper.listUserA.Count - 1;
            //    Helper.listAssetA[index].Quantity = value.Quantity;

            //});

        }


    }
}