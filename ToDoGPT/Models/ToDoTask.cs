using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ToDoGPT.Models
{
    public class ToDoTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(128)]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
        [MaxLength(512)]
        public string Resources { get; set; }
        public string Category { get; set; }
    }

    public class ViewToDoTask
    {
        public ToDoTask Object { get; set; }
        public string ListViewText => $"{Object.Title}";
        public string ListViewDetailOngoing => $"Due: {Object.DueDate.Date}\nCreated at {Object.CreationDate}";
        public string ListViewDetailCompleted => $"{ListViewDetailOngoing}\nCompleted at{Object.CompletionDate}";
        public DateTime ListViewOrderKey => Object.DueDate;
    }
}
