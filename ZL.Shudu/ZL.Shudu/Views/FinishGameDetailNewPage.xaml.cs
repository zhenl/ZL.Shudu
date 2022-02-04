using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZL.Shudu.ViewModels;

namespace ZL.Shudu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishGameDetailNewPage : ContentPage
    {
        FinishGameDetailViewModel _viewModel;
        public FinishGameDetailNewPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FinishGameDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}