using SQLite;
using StringConverter.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StringConverter.Services
{
    public class SQLiteProvider
    {
        private SQLiteAsyncConnection database;
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

        public async Task RegisterTable()
        {
            await database.CreateTableAsync<History>();
        }
    }
}
