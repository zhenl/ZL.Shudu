using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZL.Shudu.ViewModels;
using ZL.Shudu.Views;

namespace ZL.Shudu
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
