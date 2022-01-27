using SQLite;
using System;
using Xamarin.Forms;
using ZL.Shudu.Views;

namespace ZL.Shudu.Services
{
    public class FinishGame
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 游戏的初始状态
        /// </summary>
        public string Sudoku { get; set; }
        /// <summary>
        /// 游戏的结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 游戏过程
        /// </summary>
        public string Steps { get; set; }
        /// <summary>
        /// 游戏完成日期
        /// </summary>
        public DateTime PlayDate { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public long TotalTime { get; set; }

        public Command<FinishGame> ItemTapped { get; }

        public FinishGame()
        {
            ItemTapped = new Command<FinishGame>(OnItemSelected);
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
