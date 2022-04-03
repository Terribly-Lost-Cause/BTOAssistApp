using System.Collections.Generic;
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
            CreateTableResult dataTable = await Database.CreateTableAsync<PhoneInfo>();
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

        public Task<BTO> GetBTODetailAsync(string id)
        {
            return Database.Table<BTO>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<BTO>> GetBTOPopularityAsync()
        {
            return Database.Table<BTO>().OrderByDescending(x => x.Applicants).ToListAsync();
        }

        public Task<int> AddBTOAsync(BTO item)
        {
            return Database.InsertAsync(item);
        }

        public Task<int> AddDataAsync(PhoneInfo item)
        {
            var get = Database.Table<PhoneInfo>().Where(i => item.deviceID == item.deviceID).FirstOrDefaultAsync();
            if (get == null){
                return Database.InsertAsync(item);
            }
            else
            {
                return Database.UpdateAsync(item);
            }
        }

        public Task<PhoneInfo> GetBTODataAsync(string id)
        {
            return Database.Table<PhoneInfo>().Where(i => i.deviceID == id).FirstOrDefaultAsync();
        }

        public Task<List<PhoneInfo>> GetAllPhoneInfoAsync()
        {
            return Database.Table<PhoneInfo>().ToListAsync();
        }
    }
}
