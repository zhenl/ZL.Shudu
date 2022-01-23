using SQLite;
using System;

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


    }
}
