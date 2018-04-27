using PillAssistant_Xamarin.Data;
using System.Collections.Generic;

namespace PillAssistant_Xamarin.ViewModels.Pages
{
    public class ReminderListItem
    {
        public int Id;
        public string PillPhoto { get; set; }
        public string Name { get; set; }
        public string NextDateToConsume { get; set; }

    }

    public class HomePageViewModel : ObservableObject
    {
        public List<ReminderListItem> ReminderList { get; set; }

        public void PopulateReminderList()
        {
            ReminderList = new List<ReminderListItem>();

            foreach (var item in new ReminderItemDatabase().GetItems())
            {
                ReminderList.Add(new ReminderListItem { Id = item.PillId, PillPhoto = item.PillType + ".png", Name = item.PillName, NextDateToConsume = item.NextDateToBeConsumed.ToString("d") });

            }
        }
    }
}
