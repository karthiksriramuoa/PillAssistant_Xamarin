using System;

namespace PillAssistant_Xamarin
{
    public interface IReminderService
    {
        void Remind(DateTime dateTime, string title, string message);
    }
}
