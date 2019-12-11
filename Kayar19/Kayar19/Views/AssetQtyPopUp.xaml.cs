using Kayar19.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetQtyPopUp
    {
        public AssetQtyPopUp()
        {
            InitializeComponent();

        }

        public void AddQuantity(Object Sender, EventArgs args)
        {
            MessagingCenter.Send(new Asset() { Quantity = ItemQuantity.Text.ToString() }, "PopUpData");
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}