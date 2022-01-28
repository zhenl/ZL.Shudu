using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZL.Shudu.Models;

namespace ZL.Shudu.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishGameDetailPage : ContentPage
    {
        private static int[,] chess = new int[9, 9];
        private Button[,] buttons = new Button[9, 9];
        int[,] fchess = new int[9, 9];

        private List<string> steps = new List<string>();
        private int currentsetp = 0;

        private long currentDiffer = 0;
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
                    var game = App.Database.GetFinishGameAsync(currentId).Result;
                    if (game != null) OpenHistory(game);
                }

            }
        }

        public string Title { get; set; } = "游戏记录";
        public FinishGameDetailPage()
        {
            InitializeComponent();
            SetLayout();
        }

        private void SetResult(bool beginonly = false)
        {
            currentsetp = beginonly ? 0 : steps.Count - 1;
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var btn = buttons[i, j];
                    if (chess[i, j] > 0)
                    {
                        btn.Text = chess[i, j].ToString();
                        btn.TextColor = Color.Red;
                    }
                    else
                    {

                        btn.Text = beginonly ? "" : fchess[i, j].ToString();
                        btn.TextColor = Color.Blue;

                    }

                }
            }
        }

        private void SetLayout()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    int m = i / 3;
                    int n = j / 3;
                    var btn = new Button();
                    var c = new Color(0.9, 0.9, 0.9);
                    var c1 = Color.Green;
                    if ((m + n) % 2 == 0)
                    {
                        c = new Color(1, 1, 1);

                    }
                    btn.BackgroundColor = c;
                    btn.Padding = 0;
                    btn.Margin = 0;
                    btn.FontSize = 20;
                    myGrid.Children.Add(btn, i, j);

                    buttons[i, j] = btn;


                }
            }
        }


        private void btn_Begin_Clicked(object sender, EventArgs e)
        {
            SetResult(true);
        }

        private void btn_End_Clicked(object sender, EventArgs e)
        {
            SetResult(false);
        }

        private void btn_Forward_Clicked(object sender, EventArgs e)
        {

            SetStep(false);
            currentsetp++;
        }

        private void btn_Back_Clicked(object sender, EventArgs e)
        {

            SetStep(true);
            currentsetp--;
        }

        private void SetStep(bool isback)
        {
            if (currentsetp < 0) currentsetp = 0;
            if (currentsetp >= steps.Count) currentsetp = steps.Count - 1;
            var laststep = steps[currentsetp];
            var arr = laststep.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(arr[0]), y = int.Parse(arr[1]), num = int.Parse(arr[2]);
            if (isback) buttons[x, y].Text = "";
            else buttons[x, y].Text = fchess[x, y].ToString();
        }

        private void OpenHistory(FinishGame item)
        {
            try
            {
                if (item!=null)
                {
                   
                  
                    for (var i = 0; i < 9; i++)
                    {
                        for (var j = 0; j < 9; j++)
                        {
                            chess[i, j] = int.Parse(item.Sudoku.Substring(i * 9 + j, 1));
                            fchess[i, j] = int.Parse(item.Result.Substring(i * 9 + j, 1));
                        }
                    }
                    SetResult();
                    if (!string.IsNullOrEmpty(item.Steps))
                    {
                        var steparr = item.Steps.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        steps.Clear();
                        steps.AddRange(steparr);
                    }
                   
                    currentDiffer =item.TotalTime;
                    
                    var diff =  currentDiffer / 10000 / 1000 / 60;
                    lbTime.Text = diff + "分钟";
                }
            }
            catch (Exception ex)
            {

                lbMessage.Text = ex.Message;
            }
        }
    }
}