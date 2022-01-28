using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZL.Shudu.Models;
using ZL.Sudoku.Lib;

namespace ZL.Shudu.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameEdit : ContentPage
    {
        private bool IsSaved=false;
        private int currentId = 0;
        private Button[,] buttons = new Button[9, 9];
        private Button[,] numbuttons = new Button[2, 5];
        private int[,] lastinput;
        private Button currentButton;
        private Button currentNumBtn;

        public string ItemId
        {
            get
            {
                return currentId.ToString();
            }
            set
            {
                currentId = int.Parse( value);
                if (currentId > 0)
                {
                    var game = App.Database.GetGameAsync(currentId).Result;
                    if(game != null) EditGame(game);
                }
                
            }
        }
        public GameEdit()
        {
            InitializeComponent();
            SetLayout();
            SetNumButtons();
        }
        internal void EditGame(InputGameInfo game)
        {
            currentId = game.ID;
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++)
                {
                    var val = int.Parse(game.Sudoku.Substring(i * 9 + j, 1));
                    buttons[i, j].Text = val > 0 ? val.ToString() : "";
                    buttons[i, j].IsEnabled = true;

                }
        }

        private void SetNumButtons()
        {
            var num = 1;
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var btn = new Button();
                    if (num == 10)
                    {
                        btn.Text = "清除";
                        btn.Clicked += Clear_Clicked;
                        btn.FontSize = 15;
                    }
                    else
                    {
                        btn.Text = num.ToString();
                        btn.Clicked += Num_Clicked;
                        btn.FontSize = 16;
                    }
                    btn.Padding = 0;
                    grdNumber.Children.Add(btn, j, i);
                    numbuttons[i, j] = btn;
                    num++;
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
                    if ((m + n) % 2 == 0)
                    {
                        c = new Color(0.7, 0.7, 0.7);
                    }
                    btn.BackgroundColor = c;
                    btn.Padding = 0;
                    btn.Margin = 0;
                    btn.FontSize = 20;
                    myGrid.Children.Add(btn, i, j);
                    btn.Clicked += Btn_Clicked;
                    buttons[i, j] = btn;


                }
            }
        }

        private async void btn_Save_Clicked(object sender, EventArgs e)
        {
            if(btn_Check.Text=="继续") btn_Check_Clicked(null,null);
            var str = "";
            var chess = getChess();
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++)
                {
                  str +=  chess[i, j].ToString();
                }
            var comp = new FindOneSolution(chess);
            var res = comp.Comp();
            if(res != 2)
            {
                lbMessage.Text = "不合法或者无法完成的游戏，请修改后保存";
                return;
            }
            var newgame = new InputGameInfo
            {
                Sudoku = str,
                InputDate = DateTime.Now,
                UsedInGame = true
            };
            if (currentId > 0)
            {
                newgame.ID = currentId;
                await App.Database.UpdateGameAsync(newgame);
            }
            else
            {
                currentId = await App.Database.SaveGameAsync(newgame);
            }
            lbMessage.Text = "保存成功";
        }

        private void btn_New_Clicked(object sender, EventArgs e)
        {
            lbMessage.Text = "";
            currentId = 0;
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++)
                {
                    buttons[i, j].Text = "";
                    buttons[i, j].IsEnabled = true;
                }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            currentButton = sender as Button;
            rowResult.Height = 1;
            rowButton.Height = 1;
            grdNumber.IsVisible = true;
        }
        private async void btn_Delete_Clicked(object sender, EventArgs e)
        {
            if (currentId > 0)
            {
                await App.Database.DeleteGameAsync(new InputGameInfo { ID = currentId });
                await Shell.Current.GoToAsync($"///{nameof(GameList)}");

            }
        }

        private void Num_Clicked(object sender, EventArgs e)
        {

            currentNumBtn = sender as Button;
            currentButton.Text = currentNumBtn.Text;
            myGrid.IsVisible = true;
            grdNumber.IsVisible = false;
            rowResult.Height = 40;
            rowButton.Height = 40;
            
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            if (currentButton == null) return;
            currentButton.Text = "";
            grdNumber.IsVisible = false;
            myGrid.IsVisible = true;
            rowResult.Height = 40;
            rowButton.Height = 40;
        }

        private async void btn_Back_Clicked(object sender, EventArgs e)
        {
            
            await Shell.Current.GoToAsync($"///{nameof(GameList)}");
        }

        private void btn_Check_Clicked(object sender, EventArgs e)
        {
            if (btn_Check.Text == "检查")
            {
                var cinp = getChess();
                lastinput = cinp;
                var comp = new FindOneSolution(cinp);
                var res = comp.Comp();
                var fchess = comp.Matrix;

                for (var i = 0; i < 9; i++)
                {
                    for (var j = 0; j < 9; j++)
                    {
                        var btn = buttons[i, j];
                        if (cinp[i, j] > 0)
                        {
                            btn.Text = cinp[i, j].ToString();

                        }
                        else
                        {
                            btn.Text = fchess[i, j] > 0 ? fchess[i, j].ToString() : "";
                        }
                    }
                }
                if (res == 0) lbMessage.Text = "不合法";
                else if (res == 1) lbMessage.Text = "计算不出来";
                else if (res == 2) lbMessage.Text = "计算完成";
                else lbMessage.Text = "其它错误";
                btn_Check.Text = "继续";
            }
            else
            {
                lbMessage.Text = "";
                btn_Check.Text = "检查";
                for (var i = 0; i < 9; i++)
                {
                    for (var j = 0; j < 9; j++)
                    {
                        var btn = buttons[i, j];
                        if (lastinput[i, j] > 0)
                        {
                            btn.Text = lastinput[i, j].ToString();

                        }
                        else
                        {
                            btn.Text ="";
                        }
                    }
                }
            }
        }

        private int[,] getChess()
        {
            var res = new int[9, 9];
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var btn = buttons[i, j];
                    if(string.IsNullOrEmpty(btn.Text)) res[i,j]= 0;
                    else res[i, j]=int.Parse(btn.Text);
                }
            }
            return res;
        }

    }
}