using System.ComponentModel;
using Xamarin.Forms;
using ZL.Shudu.ViewModels;

namespace ZL.Shudu.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}