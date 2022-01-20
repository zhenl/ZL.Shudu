using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZL.Shudu.Services;
using ZL.Shudu.Views;

namespace ZL.Shudu
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
