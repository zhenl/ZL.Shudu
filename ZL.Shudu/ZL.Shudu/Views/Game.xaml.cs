using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZL.Shudu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        /// <summary>
        /// 测试数独数据
        /// </summary>
        private static int[,] chess =  {
            {5,3,0,0,7,0,0,0,0},
            {6,0,0,1,9,5,0,0,0},
            {0,9,8,0,0,0,0,6,0},
            {8,0,0,0,6,0,0,0,3},
            {4,0,0,8,0,3,0,0,1},
            {7,0,0,0,2,0,0,0,6},
            {0,6,0,0,0,0,2,8,0},
            {0,0,0,4,1,9,0,0,5},
            {0,0,0,0,8,0,0,7,9}
        };
        /// <summary>
        /// 数独页面中的按钮，每个按钮代表一个格子
        /// </summary>
        private Button[,] buttons = new Button[9, 9];
        /// <summary>
        /// 数字按钮，1-9和清除
        /// </summary>
        private Button[,] numbuttons = new Button[2, 5];
        private Button currentButton;
        private Button currentNumBtn;
        public Game()
        {
            try
            {
                InitializeComponent();

                SetNumButtons();
                SetLayout();
                SetNewGame();
            }
            catch (Exception ex)
            {
                lbMessage.IsVisible = true;
                rowResult.Height = 40;
                lbMessage.Text = ex.Message;
                //throw;
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
                        btn.Clicked += btn_Clear_Clicked;
                        btn.FontSize = 15;
                    }
                    else
                    {
                        btn.Text = num.ToString();
                        btn.Clicked += btn_Num_Clicked;
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
                        c = new Color(0.5,0.5, 0.5);
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

        private void SetNewGame()
        {
            SetGame(chess);
        }

        private void SetGame(int[,] inp)
        {

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    chess[i, j] = inp[i, j];
                }
            }

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var btn = buttons[i, j];
                    if (chess[i, j] > 0)
                    {
                        btn.Text = chess[i, j].ToString();
                        btn.IsEnabled = false;
                    }
                    else
                    {
                        btn.Text = "";
                        btn.IsEnabled = true;
                    }
                }
            }
            this.lbFinish.IsVisible = false;
            this.lbTime.IsVisible = false;
            this.lbMessage.IsVisible = false;
            this.rowResult.Height = 1;
            lbTime.Text = "";
            lbMessage.Text = "";

        }
        private void Btn_Clicked(object sender, EventArgs e)
        {
            currentButton = sender as Button;
            rowResult.Height = 1;
            rowButton.Height = 1;
            grdNumber.IsVisible = true;
        }

        private void btn_Clear_Clicked(object sender, EventArgs e)
        {
            if (currentButton == null) return;
            currentButton.Text = "";
            grdNumber.IsVisible = false;
            myGrid.IsVisible = true;
            rowResult.Height = 40;
            rowButton.Height = 40;
        }

        private void btn_Num_Clicked(object sender, EventArgs e)
        {
            currentNumBtn = sender as Button;
            
            int x = -1, y = -1;
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (buttons[i, j] == currentButton)
                    {
                        x = i;
                        y = j;
                        break;
                    }

                }
            }
            var num = int.Parse(currentNumBtn.Text);

            if (!checkval(x, y, num))
            {
                return;
            }
            currentButton.Text = currentNumBtn.Text;
            myGrid.IsVisible = true;
            grdNumber.IsVisible = false;
            rowResult.Height = 40;
            rowButton.Height = 40;

            if (IsFinish())
            {
                lbFinish.IsVisible = true;
                rowResult.Height = 40;
            }
        }

        private bool checkval(int x, int y, int num)
        {
            for (var i = 0; i < 9; i++)
            {
                var buttonnum = string.IsNullOrEmpty(buttons[x, i].Text) ? 0 : int.Parse(buttons[x, i].Text);
                if (i != y && buttonnum == num) return false;
            }

            for (var i = 0; i < 9; i++)
            {
                var buttonnum = string.IsNullOrEmpty(buttons[i, y].Text) ? 0 : int.Parse(buttons[i, y].Text);
                if (i != x && buttonnum == num) return false;
            }

            int m = x / 3;
            int n = y / 3;
            for (int i = m * 3; i < (m + 1) * 3; i++)
            {
                for (int j = n * 3; j < (n + 1) * 3; j++)
                {
                    var buttonnum = string.IsNullOrEmpty(buttons[i, j].Text) ? 0 : int.Parse(buttons[i, j].Text);
                    if (i != x && j != y && buttonnum == num) return false;
                }
            }

            return true;
        }

        private bool IsFinish()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (string.IsNullOrEmpty(buttons[i, j].Text)) return false;
                }
            }
            return true;
        }
    }
}