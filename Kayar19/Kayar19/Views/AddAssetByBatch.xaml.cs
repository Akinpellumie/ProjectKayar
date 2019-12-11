using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssetByBatch : ContentPage
    {
        public string item_id { get; set; }
        public AddAssetByBatch()
        {
            InitializeComponent();
        }
        private async void UploadCSV_Clicked(object sender, EventArgs e)
        {
            var bf = await Plugin.FilePicker.CrossFilePicker.Current.PickFile(allowedTypes: new string[] { null });


            Plugin.FileUploader.Abstractions.FileBytesItem bfitem = new Plugin.FileUploader.Abstractions.FileBytesItem("csvDoc", bf.DataArray, bf.FileName);

            Plugin.FileUploader.Abstractions.FilePathItem fpitem = new Plugin.FileUploader.Abstractions.FilePathItem("csvDoc", bf.FilePath);
            indicator.IsVisible = true;
            indicator.IsRunning = true;
            Plugin.FileUploader.Abstractions.FileUploadResponse k = null;
            try
            {


                k = await Plugin.FileUploader.CrossFileUploader.Current.UploadFileAsync(Helper.addNewAsseturl, bfitem, new Dictionary<string, string>() { { "Authorization", Helper.userprofile.token } }, new Dictionary<string, string>() { { "item_id", this.item_id } });
            }
            catch (Exception x)
            {
                //Acr.UserDialogs.UserDialogs.Instance.Toast(new Acr.UserDialogs.ToastConfig("A fatal error occured, check your internet and try again ."));
                indicator.IsRunning = false;
                return;
            }
            //var k = await Plugin.FileUploader.CrossFileUploader.Current.UploadFileAsync(helper.uploadcustomerloandocurl,fpitem, new Dictionary<string, string>() { { "Token", helper.userprofile.token } }, new Dictionary<string, string>() { { "applicationId", this.applicationid } });

            if (k.StatusCode == 200)
            {
                await DisplayAlert("Kayar", "Upload was successful", "ok");
                indicator.IsVisible = false;
                indicator.IsRunning = false;
                await Navigation.PushAsync(new ViewAssets());
            }
            else if (k.StatusCode == 401)
            {
                indicator.IsVisible = false;
                indicator.IsRunning = false;
                Application.Current.MainPage = new AppShellSTK();
            }
            else
            {
                indicator.IsVisible = false;
                indicator.IsRunning = false;
                await DisplayAlert("Kayar", k.Message, "ok");
            }

        }

    }
}