using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZL.Shudu.Models;

namespace ZL.Shudu.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class FinishGameDetailViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; }

        public ObservableCollection<ButtonInfo> Buttons { get; set; }

        public FinishGameDetailViewModel()
        {
            Title = "完成游戏";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Buttons = new ObservableCollection<ButtonInfo>();
        }

        private string _itemId;
        public string ItemId { get { return _itemId; } set { 
            _itemId = value;
               

               
            } }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Buttons.Clear();
                for (var i = 0; i <9; i++)
                    for (var j = 0; j < 9; j++)
                    {
                        Buttons.Add(new ButtonInfo { Colunm = i, Row = j, Text = i.ToString(), Enabled = true });
                    }
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
