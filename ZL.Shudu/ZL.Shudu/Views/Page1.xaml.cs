using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZL.Shudu.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private string itemId;
        public string ItemId { 
            get { return itemId; } 
            set { 
                itemId = value; 
            }
        }
        public Page1()
        {
            InitializeComponent();
        }
    }
}