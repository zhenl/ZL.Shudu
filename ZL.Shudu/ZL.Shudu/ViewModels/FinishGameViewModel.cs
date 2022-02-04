using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZL.Shudu.Models;
using ZL.Shudu.Views;

namespace ZL.Shudu.ViewModels
{
    public class FinishGameViewModel:BaseViewModel
    {
        private FinishGame _selectedItem;
        public ObservableCollection<FinishGame> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command<FinishGame> ItemTapped { get; }

        public FinishGameViewModel()
        {
            Title = "完成游戏列表";
            Items = new ObservableCollection<FinishGame>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<FinishGame>(OnItemSelected);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.Database.GetFinishGamesAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public FinishGame SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async void OnItemSelected(FinishGame item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(FinishGameDetailPage)}?{nameof(FinishGameDetailPage.ItemId)}={item.Id}");
        }
    }
}
