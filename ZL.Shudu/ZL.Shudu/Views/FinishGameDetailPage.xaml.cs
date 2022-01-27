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
    public partial class FinishGameDetailPage : ContentPage
    {
        private int currentId;
        public string ItemId
        {
            get
            {
                return currentId.ToString();
            }
            set
            {
                currentId = int.Parse(value);
                if (currentId > 0)
                {
                    //var game = App.Database.GetGameAsync(currentId).Result;
                    //if (game != null) EditGame(game);
                }

            }
        }
        public FinishGameDetailPage()
        {
            InitializeComponent();
        }
    }
}