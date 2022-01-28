using SQLite;
using System;

namespace ZL.Shudu.Models
{
    public class InputGameInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        /// <summary>
        /// 游戏的初始状态
        /// </summary>
        public string Sudoku { get; set; }
        /// <summary>
        /// 输入日期
        /// </summary>
        public DateTime InputDate { get; set; }
        /// <summary>
        /// 是否在游戏中使用
        /// </summary>
        public bool UsedInGame { get; set; }
    }
}
