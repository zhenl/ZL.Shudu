using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZL.Shudu.Models;

namespace ZL.Shudu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishGameList : ContentPage
    {
        public ObservableCollection<FinishGame> Items { get; set; }

        public Command<FinishGame> ItemTapped { get; }
        public FinishGameList()
        {
            InitializeComponent();

            ItemTapped = new Command<FinishGame>(OnItemSelected);
        }

        protected override async void OnAppearing()
        {
            await RefreshList();
        }

        public async Task RefreshList()
        {
            Items = await GetItems();
            ItemsListView.ItemsSource = Items;
            ItemsListView.IsVisible = true;
        }
        public async Task<ObservableCollection<FinishGame>> GetItems()
        {
            var items = new ObservableCollection<FinishGame>();

            var lst = await App.Database.GetFinishGamesAsync();

            foreach (var obj in lst)
            {
                items.Add(obj);
            }
            return items;
        }

        async void OnItemSelected(FinishGame item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FinishGameDetailPage)}?{nameof(FinishGameDetailPage.ItemId)}={item.Id}");
        }
    }
}