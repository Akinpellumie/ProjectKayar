using Kayar19.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Serializable]
    public class OperationCanceledException : SystemException
    {
        CancellationTokenSource cts = new CancellationTokenSource();
    }
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            App.StartCheckIfInternet(lbl_NoInternet, this);
        }

        public async void LoginClicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            User users = new User(txtUsername.Text, txtPassword.Text)
            {
                username = txtUsername.Text,
                password = txtPassword.Text
            };

            if (users.CheckInformation())
            {


                var json = JsonConvert.SerializeObject(users);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Token", HelperAppSettings.Token);
                var loginEndpoint = Helper.loginurl;

                var result = await client.PostAsync(loginEndpoint, content);



                string responsee = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var profile = JsonConvert.DeserializeObject<Models.LoginProfileModel>(result.Content.ReadAsStringAsync().Result);

                    Helper.userprofile = profile;
                    AppShell fpm = new AppShell();
                    HelperAppSettings.Token = profile.token;
                    HelperAppSettings.firstname = profile.firstname;
                    HelperAppSettings.lastname = profile.lastname;
                    HelperAppSettings.username = profile.username;
                    HelperAppSettings.staff_Id = profile.Staff_Id;
                    HelperAppSettings.role =string.Join(",", profile.roleId);
                    HelperAppSettings.capName = profile.capitalizedname;

                    if (profile.roleId.Contains("Admin"))
                    {

                    Application.Current.MainPage = fpm;

                    }
                    else if (profile.roleId.Contains("Storekeeper"))
                    {
                        Application.Current.MainPage = new AppShellSTK();
                    }
                    else if(profile.roleId.Contains("User"))
                    {
                        Application.Current.MainPage = new AppShellUser();
                    }
                    

                }
                else
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login Failed", "Invalid Username or Password", "Ok");

                }


            }
            else
            {
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                await DisplayAlert("Login", "Invalid Login details", "ok");

            }

        }
       public async void ForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }
    }
}