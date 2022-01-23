using System;
using System.IO;
using Xamarin.Forms;
using ZL.Shudu.Services;

namespace ZL.Shudu
{
    public partial class App : Application
    {

        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "suduku.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();
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
