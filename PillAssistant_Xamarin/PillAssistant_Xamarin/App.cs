using System;
using System.Collections.Generic;
using System.Text;
using PillAssistant_Xamarin.Models;
using PillAssistant_Xamarin.Pages;
using Xamarin.Forms;

namespace PillAssistant_Xamarin
{
    public class App : Xamarin.Forms.Application
    {
        public static Page GetMainPage()
        {
            return new NavigationPage(new HomePage());
        }

        public static ReminderItem SelectedModel { get; set; }

    }
}
