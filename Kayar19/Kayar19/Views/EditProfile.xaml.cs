using Kayar19.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class EditProfile : ContentPage
    {
        private MediaFile _mediaFile;
        public EditProfile()
        {
            InitializeComponent();
            GetUserUpdById();
        }
        public async void GetUserUpdById()
        {
            HttpClient client = new HttpClient();
            var UserUpdEndpoint = Helper.GetUsersurl + HelperAppSettings.staff_Id;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(UserUpdEndpoint);
            var UsersCnt = JsonConvert.DeserializeObject<AddedUsers>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);
            userImagePro.BindingContext = UsersCnt.data[0];
            UpdateFirstName.BindingContext = UsersCnt.data[0];
            UpdateLastName.BindingContext = UsersCnt.data[0];
            UpdateEmail.BindingContext = UsersCnt.data[0];
            UpdatePhone.BindingContext = UsersCnt.data[0];
        }

        public async void UploadImageTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "Picking  a photo is not supported", "OK");

                return;
            }
            //byte[] bytes;
              _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                CompressionQuality = 40
            });

            userImagePro.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

            //using (var memoryStream = new MemoryStream())
            //{
            //    _mediaFile.GetStream().CopyTo(memoryStream);
            //    _mediaFile.Dispose();
            //    bytes = memoryStream.ToArray();
            //}
        }
        private async void UpdateUserClicked(object sender, EventArgs e)
        {

            MultipartFormDataContent content = new MultipartFormDataContent();
            var file = _mediaFile.Path;
            if (string.IsNullOrEmpty(file) == false)
            {
                var upfilebytes = System.IO.File.ReadAllBytes(file);

                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                var name = System.IO.Path.GetFileName(file);
                baContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + System.IO.Path.GetExtension(name).Remove(0, 1));
                baContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                {
                    Name = "Image",
                    FileName = name
                };
                content.Add(baContent, "Image", name);
                AddNewUserModel update = new AddNewUserModel()
                {
                    firstname = UpdateFirstName.Text,
                    lastname = UpdateLastName.Text,
                    phonenumber = UpdatePhone.Text,
                    email = UpdateEmail.Text,
                };
                update.Image = name;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(update);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(Helper.GetUsersurl, result);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Kayar", "Profile Updated", "Ok");
                    actindicator.IsVisible = false;
                    actindicator.IsRunning = false;
                   
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

        //var httpClient = new HttpClient();
        //var updateImageUrl = Helper.Imageurl + myid;

        //var response = await httpClient.PutAsync(updateImageUrl, content);
    }
}