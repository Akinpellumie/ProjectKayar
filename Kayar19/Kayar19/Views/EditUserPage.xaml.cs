using Kayar19.Models;
using Kayar19.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        public EditUserPage(string Staff_Id)
        {
            InitializeComponent();
            //FillList();
            LoadSingleUser(Staff_Id);
            //LoadItemPrice(Staff_Id);


        }

        public async void LoadSingleUser(string Staff_Id)

        {
            var url = Helper.GetUsersurl + Staff_Id;
            HttpClient client = new HttpClient();
             client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(url);
            var UsersList = JsonConvert.DeserializeObject<AddedUsers>(result);
            SingleUserDetails.BindingContext = UsersList.data[0];
        }
        public async void UpdateUserClicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            AddNewUserModel users = new AddNewUserModel()
            {
                firstname = UpdateFirstName.Text,
                lastname = UpdateLastName.Text,
                email = UpdateEmail.Text,
                phonenumber = UpdatePhone.Text,



            }; 
            if (users.CheckInformation())

            {
                var UpdateEndpoint = Helper.addUserurl + HelperAppSettings.username + HelperAppSettings.staff_Id;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

                var json = JsonConvert.SerializeObject(users);
                HttpContent result = new StringContent(json);
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(UpdateEndpoint, result);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Profile Updated", "Ok");
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
        private async void backClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}