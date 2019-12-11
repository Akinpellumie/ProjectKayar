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
    public partial class AddNewUserPage : ContentPage
    {
        public AddNewUserPage()
        {
            InitializeComponent();
        }
        public async void CreateUserBtn_Clicked(object sender, EventArgs e)
        {
            actindicator.IsRunning = true;
            actindicator.IsVisible = true;
            AddNewUserModel users = new AddNewUserModel(txtFirstname.Text, txtLastname.Text, txtEmail.Text, txtUserNa.Text, txtPhone.Text)
            {
                username = txtUserNa.Text,
                firstname = txtFirstname.Text,
                lastname = txtLastname.Text,
                email = txtEmail.Text,
                phonenumber = txtPhone.Text,
                
           

            };
            var ny = new List<string>();
            if (Admin.IsChecked )
            {
                ny.Add("Admin");
            }
            
            if (StoreKeeper.IsChecked)
            {
                ny.Add("Storekeeper");
            }

            if(EndUser.IsChecked)
            {
                ny.Add("User");
            }

            users.roles = ny;
            if (users.CheckInformation())

            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(users);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(Helper.addUserurl, result);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Kayar", "New User Created", "Ok");
                    actindicator.IsVisible = false;
                    actindicator.IsRunning = false;
                    txtUserNa.Text = "";
                    txtFirstname.Text = "";
                txtLastname.Text = "";
                txtEmail.Text = "";
                    txtPhone.Text = "";

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