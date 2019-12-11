using Plugin.Media;
using System;
using Kayar19.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Plugin.Media.Abstractions;
using Xamarin.Forms.PlatformConfiguration;
using System.Net.Http.Headers;
using Kayar19.Models;
using Newtonsoft.Json;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePicture : ContentPage
    {
        private MediaFile _mediaFile;
        public UpdatePicture()
        {
            InitializeComponent();
            GetUserById();
            //myid = id;
            //BindingContext = new RoleViewModel();
        }

        public async void UploadImageTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "Picking  a photo is not supported", "OK");

                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                CompressionQuality = 40
            });

            userImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

        }

        private async void UpD8Image_Clicked(object sender, EventArgs e)
        {

            MultipartFormDataContent content = new MultipartFormDataContent();
            var file = _mediaFile.Path;
            if (string.IsNullOrEmpty(file) == false)
            {
                var upfilebytes = System.IO.File.ReadAllBytes(file);

                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                var name = System.IO.Path.GetFileName(file);
                baContent.Headers.ContentType = new MediaTypeHeaderValue("image/" + System.IO.Path.GetExtension(name).Remove(0, 1));
                baContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "Image",
                    FileName = name,
                    
                };
                content.Add(baContent, "Image", name);
              

                var client = new HttpClient();
                var updateImageUrl = Helper.UploadImageurl + HelperAppSettings.staff_Id;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
                var response = await client.PutAsync(updateImageUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Profile Picture Updated", "Ok");
                    indicator.IsVisible = false;
                    indicator.IsRunning = false;

                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Kayar", response.ReasonPhrase, "Ok");
                        indicator.IsVisible = false;
                        indicator.IsRunning = false;
                    }
                    else
                    {
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;
                        await DisplayAlert("Kayar", "Please try again later", "Ok");

                    }
                }
            }
        }
        public async void GetUserById()
        {
            HttpClient client = new HttpClient();
            var UserdetailEndpoint = Helper.GetUsersurl + HelperAppSettings.staff_Id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UserdetailEndpoint);
            var UsersCnt = JsonConvert.DeserializeObject<AddedUsers>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);
            userImage.BindingContext = UsersCnt.data[0];

        }
    }
    }