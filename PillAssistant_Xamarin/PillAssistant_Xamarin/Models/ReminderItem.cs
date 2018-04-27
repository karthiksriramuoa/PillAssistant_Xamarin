using System;
using SQLite;

namespace PillAssistant_Xamarin.Models
{
    public class ReminderItem : ObservableObject
    {
        public ReminderItem()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int PillId { get; set; }
        public string PillName { get; set; }
        public string PillIdentificationNumber { get; set; }

        public string PillType { get; set; }

        public bool IsReminderEnabled { get; set; }

        public DateTime NextReminder { get; set; }

        public DateTime LastConsumedDate { get; set; }

        public DateTime NextDateToBeConsumed { get; set; }

        public string Notes { get; set; }

    }
}
