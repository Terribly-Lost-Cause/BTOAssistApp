﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using BTOAssistApp.Models;

namespace BTOAssistApp.Data
{
    public class BTOAssistDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<BTOAssistDatabase> Instance = new AsyncLazy<BTOAssistDatabase>(async () =>
        {
            var instance = new BTOAssistDatabase();
            CreateTableResult result = await Database.CreateTableAsync<BTO>();
            return instance;
        });

        public BTOAssistDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<BTO>> GetBTOAsync()
        {
            return Database.Table<BTO>().ToListAsync();
        }

        public Task<List<BTO>> GetBTOPopularityAsync()
        {
            return Database.Table<BTO>().OrderByDescending(x => x.Applicants).ToListAsync();
        }

        public Task<int> AddBTOAsync(BTO item)
        {
            return Database.InsertAsync(item);
        }

        //public Task<int> DeleteItemAsync(TodoItem item)
        //{
        //    return Database.DeleteAsync(item);
        //}

        //public Task<int> DeleteAllItems<T>()
        //{
        //    return Database.DeleteAllAsync<BTO>();
        //}
    }
}