using Kayar19.Models;
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
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        public async void UpdatePasswordClicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            ChangePasswordModel users = new ChangePasswordModel()
            {
                oldPassword = txtOldPassword.Text,
                newPassword = txtNewPassword.Text,
                username = HelperAppSettings.username,

            };
            {
                var UpdateEndpoint = Helper.changePasswordurl;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(users);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(UpdateEndpoint, result);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Password Changed Successfully", "Ok");
                    await Shell.Current.Navigation.PushAsync(new LoginPage());
                    indicator.IsVisible = false;
                    indicator.IsRunning = false;
                    HelperAppSettings.Token = "";
                    HelperAppSettings.firstname = "";
                    HelperAppSettings.lastname = "";
                    HelperAppSettings.username = "";
                    HelperAppSettings.staff_Id = "";

                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Bad Request", response.ReasonPhrase, "Ok");
                        indicator.IsVisible = false;
                        indicator.IsRunning = false;
                    }
                    else
                    {
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;
                        await DisplayAlert("Connection Error", "Please try again later", "Ok");

                    }
                }
            }

        }
    }
}