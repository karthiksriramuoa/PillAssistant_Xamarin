using PillAssistant_Xamarin.Data;
using PillAssistant_Xamarin.Pages;
using PillAssistant_Xamarin.ViewModels.Cells;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PillAssistant_Xamarin.ViewModels.Pages
{
    public class EditReminderPageViewModel : PageViewModelBase
    {

        PillListPicker pillListPicker;
        public EditReminderPageViewModel()
        {

            // If SelectedModel is null then the screen is Add else Edit
            if (App.SelectedModel == null)
            {

                this.Title = "Add Pill Reminder";
                App.SelectedModel = new Models.ReminderItem();
                App.SelectedModel.LastConsumedDate = DateTime.Now;
                App.SelectedModel.NextDateToBeConsumed = DateTime.Now.AddMonths(1);
                App.SelectedModel.IsReminderEnabled = true;
                App.SelectedModel.NextReminder = App.SelectedModel.NextDateToBeConsumed.AddDays(-1);
                LastConsumedDate = DateTime.Now;

                PillType = "Aspirin";
            }
            else
            {
                this.Title = "Edit Pill Reminder";
            }

            // DEMO usage of Custom Page instead of Picker
            ChoosePillTypeCommand = new Command(async () => {

                pillListPicker = new PillListPicker();
                if (Navigation != null)
                    await Navigation.PushAsync(pillListPicker);
            });

            // Demo usage of MessagingCenter
            MessagingCenter.Subscribe<PillIntervalPickerCellViewModel>(this, "pillIntervalChanged", (sender) => {

                OnPropertyChanged("NextDateToBeConsumed");
            });
        }

        async public Task<bool> Save()
        {
            if (App.SelectedModel != null && !string.IsNullOrEmpty(App.SelectedModel.PillName) && !string.IsNullOrEmpty(App.SelectedModel.PillIdentificationNumber))
            {
                new ReminderItemDatabase().SaveItem(App.SelectedModel);
                var remiderService = DependencyService.Get<IReminderService>();
                if (remiderService != null && App.SelectedModel.IsReminderEnabled)
                    //TODO: Add 
                    remiderService.Remind(DateTime.Now.AddSeconds(25), "Pill Alert", App.SelectedModel.PillName + " is due for consumption on: " + App.SelectedModel.NextDateToBeConsumed.ToShortDateString());
            }
            else
                return false;

            return true;
        }

        public void OnAppearing()
        {
            if (pillListPicker != null)
                PillType = pillListPicker.SelectedPill;
            // if nothing was selected default to car
            if (string.IsNullOrEmpty(PillType))
                PillType = "Aspirin";
        }

        public string Name
        {
            get { return App.SelectedModel.PillName; }
            set
            {
                App.SelectedModel.PillName = value;
                OnPropertyChanged();
            }
        }

        public string PillIdentificationNo
        {
            get { return App.SelectedModel.PillIdentificationNumber; }
            set { App.SelectedModel.PillIdentificationNumber = value; OnPropertyChanged(); }
        }


        public DateTime LastConsumedDate
        {
            get { return App.SelectedModel.LastConsumedDate; }
            set
            {
                App.SelectedModel.LastConsumedDate = value;
                PillIntervalPickerCellViewModel.CalculateNextDateToBeConsumed();
                OnPropertyChanged("NextDateToBeConsumed"); OnPropertyChanged();
            }
        }


        public string NextDateToBeConsumed
        {
            get { return App.SelectedModel.NextDateToBeConsumed.ToString("d"); }
        }


        public string PillType
        {
            get { return App.SelectedModel.PillType; }
            set
            {
                App.SelectedModel.PillType = value;
                PillPhoto = PillType + ".png";
                OnPropertyChanged();
            }
        }


        public bool IsReminder
        {
            get { return App.SelectedModel.IsReminderEnabled; }
            set { App.SelectedModel.IsReminderEnabled = value; OnPropertyChanged(); }
        }

        private string pillPhoto;

        public string PillPhoto
        {
            get { return pillPhoto; }
            set { pillPhoto = value; OnPropertyChanged(); }
        }

        private Command choosePillTypeCommand;

        public Command ChoosePillTypeCommand
        {
            get { return choosePillTypeCommand; }
            set { choosePillTypeCommand = value; OnPropertyChanged(); }
        }


    }
}
