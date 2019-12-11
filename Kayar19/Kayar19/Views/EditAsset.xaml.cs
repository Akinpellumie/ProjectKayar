using Kayar19.Models;
using Kayar19.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAsset : ContentPage
    {
        public EditAsset(string item_id)
        {
            InitializeComponent();
            //FillList();
            LoadSingleItem(item_id);
            //LoadItemPrice(Staff_Id);


        }

        public async void LoadSingleItem(string item_id)

        {
            var url = Helper.GetAllAsseturl + item_id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(url);
            var AssetList = JsonConvert.DeserializeObject<GetAssetsModel>(result);
            SingleItemDetails.BindingContext = AssetList.lot[0];
        }

        private async void BackClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}