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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }
        public async void ResetPassword(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            Models.User reset = new Models.User()
            {
                email = pswdReset.Text,

            };
            if (reset.CheckInformation())

            {
                var FrgtPwdEndpoint = Helper.forgotPasswordurl;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(reset);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(FrgtPwdEndpoint, result);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "A reset link has been sent to the provided email for further procedure.", "Ok");
                    await Navigation.PushAsync(new LoginPage());
                    pswdReset.Text = "";
                    indicator.IsVisible = false;
                    indicator.IsRunning = false;

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
                        await DisplayAlert("Bad Server", "Please try again later", "Ok");

                    }
                }
            }

        }
    }
}