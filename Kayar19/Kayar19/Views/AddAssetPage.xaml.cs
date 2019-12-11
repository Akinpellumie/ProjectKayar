using Kayar19.Models;
using Kayar19.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssetPage : ContentPage
    {
        public AddAssetPage()
        {
            InitializeComponent();
           //GetCategories();
            BindingContext = new StatusViewModel();
        }
        //public async void GetCategories()

        //{
        //    HttpClient client = new HttpClient();
        //    var dashboardEndpoint = Helper.GetUsersurl;
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

        //    var result = await client.GetStringAsync(dashboardEndpoint);
        //    var UsersList = JsonConvert.DeserializeObject<GetCatModel>(result);
        //    //Users = new ObservableCollection<AddedUsers>(UsersList);

        //    PickerCategory.ItemsSource = UsersList.categories;

        //}
        public async void AddAssetClicked(object sender, EventArgs e)
        {
            actindicator.IsRunning = true;
            actindicator.IsVisible = true;
            AssetModel assets = new AssetModel()
            {
                itemName = txtAssetName.Text,
               itemDescription = txtAssetDesc.Text,
                quantity = txtAssetQty.Text,
                location = txtAssetLoc.Text,
                receivedBy = txtAssetRvdBy.Text,
                category = txtAssetCatBy.Text.Split(',').ToList(),
                comment = txtAssetComment.Text,
                broughtBy = txtAssetBrgtBy.Text,
                serialNumber = txtAssetSerialNo.Text.Split(',').ToList(),
                status = (PickerStatus.SelectedItem as  Status).Value


            };

            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(assets);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(Helper.addNewAsseturl, result);


                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Kayar", "New Asset Created", "Ok");

                    actindicator.IsVisible = false;
                    actindicator.IsRunning = false;
                    txtAssetName.Text = "";
                    txtAssetDesc.Text = "";
                    txtAssetQty.Text = "";
                    txtAssetLoc.Text = "";
                    txtAssetRvdBy.Text = "";
                    txtAssetCatBy.Text = "";
                    txtAssetComment.Text = "";
                    txtAssetBrgtBy.Text = "";
                    txtAssetSerialNo.Text = "";
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Kayar", response.ReasonPhrase, "Ok");
                        actindicator.IsVisible = false;
                        actindicator.IsRunning = false;
                    }
                    else
                    {
                        actindicator.IsRunning = false;
                        actindicator.IsVisible = false;
                        await DisplayAlert("Kayar", "Please try again later", "Ok");

                    }
                }
            }

        }
    }
}