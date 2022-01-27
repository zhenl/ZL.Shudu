using Xamarin.Forms;
using ZL.Shudu.Views;

namespace ZL.Shudu
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GameEdit), typeof(GameEdit));
            Routing.RegisterRoute(nameof(GameList), typeof(GameList));
            Routing.RegisterRoute(nameof(FinishGameList), typeof(FinishGameList));
            Routing.RegisterRoute(nameof(FinishGameDetailPage), typeof(FinishGameDetailPage));
        }

    }
}
