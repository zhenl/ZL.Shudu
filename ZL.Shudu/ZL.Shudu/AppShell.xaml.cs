using Xamarin.Forms;
using ZL.Shudu.Views;

namespace ZL.Shudu
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Page1), typeof(Page1));
        }

    }
}
