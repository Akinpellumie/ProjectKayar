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
    public partial class ViewMessagesPage : ContentPage
    {
        public ViewMessagesPage(string id)
        {
            InitializeComponent();
            LoadSingleMsg(id);
        }
        public async void LoadSingleMsg(string id)

        {
            var url = Helper.SingleNotificationurl + HelperAppSettings.username;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", Helper.userprofile.token);
            var result = await client.GetStringAsync(url);
            var MsgsList = JsonConvert.DeserializeObject<MessageModels>(result);
            SingleMsgDetails.BindingContext = MsgsList.notifications[0];
        }
    }
}