using Kayar19.Models;
using Kayar19.ViewModels;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            GetUserById();
            //GetUserRoles();
            BindingContext = new RolesViewModel();
        }

        public async void UploadImageTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "Picking  a photo is not supported", "OK");

                return;
            }
            byte[] bytes;
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40
            });

            userImagePro.Source = ImageSource.FromStream(() => file.GetStream());
            if(userImagePro.Source==null)
            {
                userImagePro.Source = ImageSource.FromFile("userProfile.png");
            }

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                bytes = memoryStream.ToArray();
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
            ProfileName.BindingContext = UsersCnt.data[0];
            userImagePro.BindingContext = UsersCnt.data[0];

            if(UsersCnt.data[0].Image==null)
            {
                userImagePro.BindingContext = ImageSource.FromFile("userProfile.png");
            }
            
        }
        async void ChngPassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }
            async void EditProfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfile());
        }
        async void UpdateImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdatePicture());
        }
        private void SignOutClicked(object sender, EventArgs e)
        {
            ContentPage fpm = new LoginPage();
            HelperAppSettings.firstname = "";
            HelperAppSettings.lastname = "";
            HelperAppSettings.Token = "";
            HelperAppSettings.username = "";
            Application.Current.MainPage = fpm;
        }
        //public async void GetUserRoles()
        //{
        //    HttpClient client = new HttpClient();
        //    var roleEndpoint = Helper.selectedUserRole + HelperAppSettings.staff_Id;
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

        //    var result = await client.GetStringAsync(roleEndpoint);
        //    var UsersRole = JsonConvert.DeserializeObject<SwitchModel>(result);
        //    PickerRoles.BindingContext = UsersRole.roles.Join(",", ",");
        //}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUserById();
        }
    }
}