using System;
using System.Collections.Generic;
using System.IO;
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

        private static int[,,] chesses =  {
           {
            {5,3,0,0,7,0,0,0,0},
            {6,0,0,1,9,5,0,0,0},
            {0,9,8,0,0,0,0,6,0},
            {8,0,0,0,6,0,0,0,3},
            {4,0,0,8,0,3,0,0,1},
            {7,0,0,0,2,0,0,0,6},
            {0,6,0,0,0,0,2,8,0},
            {0,0,0,4,1,9,0,0,5},
            {0,0,0,0,8,0,0,7,9}
            },
            {
            {3,0,2,4,0,7,9,0,1},
            {0,0,7,1,3,8,5,0,0},
            {6,0,0,0,0,0,0,0,4},
            {0,0,0,0,4,0,0,0,0},
            {0,9,4,0,0,0,1,5,0},
            {0,0,0,0,9,0,0,0,0},
            {4,0,0,0,0,0,0,0,5},
            {0,0,8,9,1,5,6,0,0},
            {5,0,9,6,0,4,7,0,2}
        },{
            {0,8,0,9,0,7,0,1,0},
            {9,0,0,3,8,6,0,0,5},
            {0,0,2,0,4,0,6,0,0},
            {5,9,0,0,0,0,0,2,6},
            {0,2,3,0,0,0,7,9,0},
            {7,4,0,0,0,0,0,5,1},
            {0,0,9,0,7,0,5,0,0},
            {4,0,0,8,5,9,0,0,2},
            {0,5,0,4,0,2,0,3,0}
        },{
            {4,0,8,0,0,0,0,0,5},
            {0,0,9,0,0,4,0,7,0},
            {3,1,0,2,0,0,8,0,0},
            {0,0,5,7,0,0,0,8,0},
            {0,0,0,0,9,0,0,0,0},
            {0,2,0,0,0,3,5,0,0},
            {0,0,2,0,0,9,0,1,4},
            {0,3,0,6,0,0,9,0,0},
            {1,0,0,0,0,0,7,0,6}
        },{
            {0,3,5,0,0,7,0,6,0},
            {8,0,6,0,1,0,0,0,7},
            {0,0,0,0,9,6,0,1,5},
            {4,0,1,8,0,3,0,0,0},
            {0,2,3,0,0,0,6,4,0},
            {0,0,0,6,0,2,3,0,1},
            {3,8,0,7,2,0,0,0,0},
            {6,0,0,0,3,0,7,0,9},
            {0,7,0,9,0,0,4,8,0}
        },{
            {0,0,0,7,0,4,0,0,0},
            {0,0,0,8,0,5,0,0,0},
            {5,8,0,0,0,0,0,4,7},
            {7,6,0,0,0,0,0,3,9},
            {9,2,0,6,1,3,0,7,8},
            {8,5,0,0,0,0,0,2,1},
            {1,7,0,0,0,0,0,9,3},
            {0,0,0,3,0,6,0,0,0},
            {0,0,0,1,0,7,0,0,0}
        },{
            {0,7,0,0,9,0,2,0,0},
            {0,0,0,0,0,7,0,0,5},
            {2,0,4,3,5,0,7,0,0},
            {0,5,0,0,4,0,9,0,0},
            {9,0,3,5,0,1,4,0,8},
            {0,0,7,0,6,0,0,1,0},
            {0,0,8,0,1,9,3,0,2},
            {7,0,0,2,0,0,0,0,0},
            {0,0,2,0,3,0,0,9,0}
        },{
            {3,0,2,4,0,7,9,0,1},
            {0,0,7,1,3,8,5,0,0},
            {6,0,0,0,0,0,0,0,4},
            {0,0,0,0,4,0,0,0,0},
            {0,9,4,0,0,0,1,5,0},
            {0,0,0,0,9,0,0,0,0},
            {4,0,0,0,0,0,0,0,5},
            {0,0,8,9,1,5,6,0,0},
            {5,0,9,6,0,4,7,0,2}
        }
        };
        Random ra = null;

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

        private DateTime dtBegin;
        private Stack<string> steps = new Stack<string>();
        private long currentDiffer = 0;//记录的时间差


        public Game()
        {
            try
            {
                InitializeComponent();

                SetNumButtons();
                SetLayout();
                if (!OpenCurrent()) SetNewGame();

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
            int k;
            var lst = chesses;
            var leng = lst.GetLength(0);

            if (ra == null)
            {
                ra = new Random();
            }

            k = ra.Next(0, leng);
           
            var mychess = new int[9, 9];
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++)
                {
                    mychess[i, j] = chesses[k, i, j];
                }

            SetGame(mychess);
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
                        //btn.BackgroundColor = new Color(0.5, 0.5, 0.5);
                        btn.TextColor = Color.Gray;
                    }
                    else
                    {
                        btn.Text = "";
                        btn.IsEnabled = true;
                    }
                    int m = i / 3;
                    int n = j / 3;
                   
                    var c = new Color(0.9, 0.9, 0.9);
                    if ((m + n) % 2 == 0)
                    {
                        c = new Color(0.7, 0.7, 0.7);
                    }
                    btn.BackgroundColor = c;
                }
            }
            steps.Clear();
            dtBegin = DateTime.Now;

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
            steps.Push(x + "," + y + "," + num);
            currentButton.Text = currentNumBtn.Text;
            myGrid.IsVisible = true;
            grdNumber.IsVisible = false;
            rowResult.Height = 40;
            rowButton.Height = 40;

            if (IsFinish())
            {
                lbFinish.IsVisible = true;
                lbTime.IsVisible = true;
                rowResult.Height = 40;
                var diff = (DateTime.Now.Ticks - dtBegin.Ticks + currentDiffer) / 10000 / 1000 / 60;
                lbTime.Text = diff + "分钟";
                string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shudu_current.txt");
                File.Delete(_fileName);
            }
        }

        private void btn_Reset_Clicked(object sender, EventArgs e)
        {
            if (steps.Count > 0)
            {
                var laststep = steps.Pop();
               
                var arr = laststep.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(arr[0]), y = int.Parse(arr[1]), num = int.Parse(arr[2]);
                buttons[x, y].Text = "";
            }
        }

        private void btn_NewGame_Clicked(object sender, EventArgs e)
        {
            SetNewGame();
            lbFinish.IsVisible = false;
            lbTime.Text = "";
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

        private void Save(string filename)
        {
            try
            {
                var strchess = "";
                for (var i = 0; i < 9; i++)
                {
                    for (var j = 0; j < 9; j++)
                    {
                        strchess += chess[i, j].ToString();
                    }
                }
                strchess += " ";
                for (var i = 0; i < 9; i++)
                {
                    for (var j = 0; j < 9; j++)
                    {
                        strchess += string.IsNullOrEmpty(buttons[i, j].Text) ? "0" : buttons[i, j].Text;
                    }
                }
                strchess += " ";
                foreach (var str in steps)
                {
                    strchess += str + ";";
                }
                strchess += " ";
                strchess += (DateTime.Now - dtBegin).Ticks.ToString();
                string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
                File.WriteAllText(_fileName, strchess);
            }
            catch (Exception ex)
            {

                lbMessage.Text = ex.Message;
            }
        }

        private bool OpenCurrent()
        {
            try
            {
                string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shudu_current.txt");
                if (File.Exists(_fileName))
                {
                    var strchess = File.ReadAllText(_fileName);
                    int[,] fchess = new int[9, 9];
                    var arr = strchess.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < 9; i++)
                    {
                        for (var j = 0; j < 9; j++)
                        {
                            chess[i, j] = int.Parse(arr[0].Substring(i * 9 + j, 1));
                            fchess[i, j] = int.Parse(arr[1].Substring(i * 9 + j, 1));
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
                            }
                            else
                            {
                                btn.Text = fchess[i, j] > 0 ? fchess[i, j].ToString() : "";
                            }
                            btn.IsEnabled = chess[i, j] == 0;
                            int m = i / 3;
                            int n = j / 3;

                            var c = new Color(0.9, 0.9, 0.9);
                            if ((m + n) % 2 == 0)
                            {
                                c = new Color(0.7, 0.7, 0.7);
                            }
                            btn.BackgroundColor = c;
                        }
                    }
                    if (arr.Length > 2)
                    {
                        var steparr = arr[2].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        steps.Clear();
                        foreach(var step in steparr) steps.Push(step);
                    }
                    if (arr.Length > 3)
                    {
                        currentDiffer = long.Parse(arr[3]);
                    }
                    dtBegin = DateTime.Now;
                    return true;
                }
            }
            catch (Exception ex)
            {

                lbMessage.Text = ex.Message;
            }
            return false;
        }

        protected override void OnDisappearing()
        {
            Save("shudu_current.txt");
        }
    }
}