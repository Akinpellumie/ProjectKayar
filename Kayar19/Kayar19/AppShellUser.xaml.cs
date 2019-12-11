using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kayar19
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShellUser : Xamarin.Forms.Shell
    {
        public AppShellUser()
        {
            InitializeComponent();
        }
    }
}