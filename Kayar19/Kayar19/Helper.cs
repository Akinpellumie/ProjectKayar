using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Kayar19.Models;
using System.Threading.Tasks;

namespace Kayar19
{
    public class Helper
    {
        public static string domainurl
        {
            get
            {
               
                //remote
                return "http://192.168.1.113:3000";
            }
        }

        public static string selectedUserRole { get { return Helper.domainurl + "/role/"; } }
        public static string loginurl { get { return Helper.domainurl + "/login"; } }

        public static string logouturl { get { return Helper.domainurl + "/logout"; } }

        public static string GetUsersurl { get { return Helper.domainurl + "/Users/"; } }

        public static string UploadImageurl { get { return Helper.domainurl + "/Users/Image/"; } }

        public static string addUserurl { get { return Helper.domainurl + "/User/"; } }
        public static string requestAsseturl { get { return Helper.domainurl + "/request/"; } }
        public static string assignAsseturl { get { return Helper.domainurl + "/assign/"; } }

        public static string SearchUsersurl { get { return Helper.domainurl + "/Users?name="; } }
        public static string updateUserurl { get { return Helper.domainurl + "/User/"; } }
        public static string userCounturl { get { return Helper.domainurl + "/count/Users/"; } }

        public static string assetCounturl { get { return Helper.domainurl + "/count/Assets/"; } }

        public static string requestCounturl { get { return Helper.domainurl + "/requestCount/"; } }

        public static string STKAssignedCounturl { get { return Helper.domainurl + "/assignedAssetCount/"; } }

        public static string userRequestCounturl { get { return Helper.domainurl + "/request/staff/"; } }

        public static string StaffAssetCounturl { get { return Helper.domainurl + "/Staff/Asset/"; } }
        public static string GetAllAsseturl { get { return Helper.domainurl + "/Assets/"; } }
        public static string Imageurl { get { return Helper.domainurl + "/images/users/"; } }
        public static string changePasswordurl { get { return Helper.domainurl + "/passwordChange/"; } }

        public static string addNewAsseturl { get { return Helper.domainurl + "/Assets/"; } }
        public static string SearchAsseturl { get { return Helper.domainurl + "/Assets?itemName="; } }
        public static string GetNotificationByIdurl { get { return Helper.domainurl + "/notification/"; } }
        public static string UserAssignedurl { get { return Helper.domainurl + "/Staff/Asset/"; } }
        public static string forgotPasswordurl { get { return Helper.domainurl + "/forgotPassword/"; } }

        public static string SingleNotificationurl { get { return Helper.domainurl + "/notification?to="; } }
        public static string UnreadNotificationurl { get { return Helper.domainurl + "/notification/count?to="; } }
        public static string UnreadCounturl { get { return "&is_Read=0"; } }


        public static List<Models.AddedUsers> AllUsers { get; set; }


        public async void getTotalUsers(string userid)
        {
            HttpClient _client;
            _client = new HttpClient();

            var uri = new Uri(string.Format(Helper.GetUsersurl + userid, string.Empty));

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Token", Helper.userprofile.token);



            HttpResponseMessage response = null;


            response = await _client.GetAsync(uri);



            if (response.IsSuccessStatusCode)
            {
                var profile = JsonConvert.DeserializeObject<List<Models.AddedUsers>>(response.Content.ReadAsStringAsync().Result);


                Helper.AllUsers = profile;



            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Xamarin.Forms.Application.Current.MainPage = new Views.LoginPage();
                Helper.AllUsers = null;
            }

        }

        public static Models.LoginProfileModel userprofile { get; set; }

        public static List<Models.GetAssetsModel> assetslist { get; set; }

        public static List<Models.Asset> listAssetA = new List<Models.Asset>();
        public static List<Models.GetAssetsModel> listAssetC = new List<Models.GetAssetsModel>();
        public static List<Models.LotAsset> listAssetB = new List<Models.LotAsset>();
        public static List<Models.Data> listUserA = new List<Models.Data>();

    }
}
