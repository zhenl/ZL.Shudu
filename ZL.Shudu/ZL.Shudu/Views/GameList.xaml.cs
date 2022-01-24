using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZL.Shudu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameList : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public GameList()
        {
            InitializeComponent();
           
        }

        protected override async void OnAppearing()
        {
            await RefreshList();
        }

        public async Task RefreshList()
        {
            Items = await GetItems();
            MyListView.ItemsSource = Items;
            MyListView.IsVisible = true;
        }

        public async Task<ObservableCollection<string>> GetItems()
        {
            var items = new ObservableCollection<string>();

            var lst = await App.Database.GetGamesAsync();

            foreach (var obj in lst)
            {
                items.Add(obj.ID.ToString());
            }
            return items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(GameEdit)}?{nameof(GameEdit.ItemId)}={e.Item}");
        }

        async void btn_NewGame_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(GameEdit)}?{nameof(GameEdit.ItemId)}=0");
        }
    }
}