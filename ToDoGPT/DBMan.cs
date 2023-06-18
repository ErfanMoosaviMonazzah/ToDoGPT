using CommunityToolkit.Maui.Core.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoGPT.Models;

namespace ToDoGPT
{
    public class DBMan
    {
        public string DatabasePath { get; }
        public string StatusMessage { get; }
        public SQLiteConnection Connection { get; }

        public DBMan(string dbName)
        {
            DatabasePath = Path.Combine(FileSystem.AppDataDirectory, dbName);
            Connection = new(DatabasePath);
            Connection.CreateTable<ToDoTask>();
        }

        public ObservableCollection<ViewToDoTask> ObsTasksOngoing()
            => Connection.Table<ToDoTask>()
            .Where(x => !x.IsCompleted)
            .Select(x => new ViewToDoTask { Object = x })
            .OrderBy(x => x.ListViewOrderKey)
            .ToObservableCollection();
        public ObservableCollection<ViewToDoTask> ObsTasksCompleted()
            => Connection.Table<ToDoTask>()
            .Where(x => x.IsCompleted)
            .Select(x => new ViewToDoTask { Object = x })
            .OrderBy(x => x.ListViewOrderKey)
            .ToObservableCollection();
    }
}
