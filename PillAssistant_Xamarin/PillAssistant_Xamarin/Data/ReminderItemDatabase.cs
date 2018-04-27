using System;
using System.Collections.Generic;
using PillAssistant_Xamarin.Models;
using SQLite;
using System.Linq;
using System.IO;

namespace PillAssistant_Xamarin.Data
{
    public class ReminderItemDatabase
    {

        static string DatabasePath
        {
            get
            {
                var sqliteFilename = "PillAssistant_Xamarin.db3";
            #if __IOS__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
                var path = Path.Combine(libraryPath, sqliteFilename);
            #else
            #if __ANDROID__
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, sqliteFilename);
            #endif
            #endif
                return path;
            }
        }

        static object locker = new object();

        static SQLiteConnection database = new SQLiteConnection(databasePath: DatabasePath);

        public ReminderItemDatabase()
        {
            // create the tables
            database.CreateTable<ReminderItem>();
        }

        public IEnumerable<ReminderItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<ReminderItem>() select i).ToList();
            }
        }

        public ReminderItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<ReminderItem>().FirstOrDefault(x => x.PillId == id);
            }
        }

        public int SaveItem(ReminderItem item)
        {
            lock (locker)
            {
                if (item.PillId != 0)
                {
                    database.Update(item);
                    return item.PillId;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<ReminderItem>(id);
            }
        }
    }
}
