using Kayar19.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StkNotification : ContentPage
    {
        public StkNotification()
        {
            InitializeComponent();
            GetMsgs();

        }
        public async void GetMsgs()

        {
            indicator.IsRunning = true;
            indicator.IsVisible = true;


            HttpClient client = new HttpClient();
            //var MsgUser = e.Models.Sa
            var messageEndpoint = Helper.SingleNotificationurl + HelperAppSettings.username;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);

            var result = await client.GetStringAsync(messageEndpoint);
            var MessageList = JsonConvert.DeserializeObject<MessageModels>(result);
            //Users = new ObservableCollection<AddedUsers>(UsersList);

            MsgList.ItemsSource = MessageList.notifications;

            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }
        async void NotificationTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedMsg = e.Item as Models.Notifications;
            await DisplayAlert("Msg", "Read Notification", "ok" + selectedMsg.id);
            //await Shell.Current.Navigation.PushAsync(new EditUserPage(selectedMsg.id));

        }
    }
}