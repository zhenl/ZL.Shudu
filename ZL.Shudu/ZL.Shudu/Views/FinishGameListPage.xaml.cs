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
    public partial class FinishGameListPage : ContentPage
    {
        FinishGameViewModel _viewModel;
        public FinishGameListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FinishGameViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}