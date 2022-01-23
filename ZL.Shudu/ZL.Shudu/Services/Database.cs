using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZL.Shudu.Services
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<InputGameInfo>().Wait();
            _database.CreateTableAsync<FinishGame>().Wait();
        }

        #region InputGameInfo
        public Task<List<InputGameInfo>> GetGamesAsync()
        {
            return _database.Table<InputGameInfo>().ToListAsync();
        }

        public Task<int> SaveGameAsync(InputGameInfo game)
        {
            return _database.InsertAsync(game);
        }

        public Task<int> UpdateGameAsync(InputGameInfo game)
        {
            return _database.UpdateAsync(game);
        }

        public Task<int> DeleteGameAsync(InputGameInfo game)
        {
            return _database.DeleteAsync(game);
        }

        #endregion

        #region FinishGame
        public Task<List<FinishGame>> GetFinishGamesAsync()
        {
            return _database.Table<FinishGame>().ToListAsync();
        }

        public Task<int> SaveFinishGameAsync(FinishGame game)
        {
            return _database.InsertAsync(game);
        }

        public Task<int> UpdateFinishGameAsync(FinishGame game)
        {
            return _database.UpdateAsync(game);
        }

        public Task<int> DeleteFinishGameAsync(FinishGame game)
        {
            return _database.DeleteAsync(game);
        }
        #endregion
    }
}
