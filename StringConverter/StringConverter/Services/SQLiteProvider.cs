using SQLite;
using StringConverter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StringConverter.Services
{
    public class SQLiteProvider
    {
        private SQLiteAsyncConnection database;
        private bool isInit = false;
        private SQLiteProvider()
        {
            database = new SQLiteAsyncConnection(
                  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StringConverter.db3"), true);
        }
        
        private static SQLiteProvider instace;
        public static SQLiteProvider Instace
        {
            get
            {
                if (instace == null) instace = new SQLiteProvider();
                return instace;
            }
        }

        public SQLiteAsyncConnection Database
        {
            get
            {
                return database;
            }
        }
        public bool IsInit { get => isInit; }

        public async Task InitDatabase()
        {
            await database.CreateTableAsync<History>();
            isInit = true;
        }

        public Task<List<History>> GetAllHistory()
        {
            return database.Table<History>().ToListAsync();
        }

        public Task<int> Add(History history)
        {
            return database.InsertAsync(history);
        }
        public Task<int> Delete(History history)
        {
            return database.DeleteAsync(history);
        }
    }
}
