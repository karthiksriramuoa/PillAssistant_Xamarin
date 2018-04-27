﻿using PillAssistant_Xamarin.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PillAssistant_Xamarin.ViewModels.Cells
{
    public class PillIntervalPickerCellViewModel : ObservableObject, IPickerCellViewModel
    {
        public const string Monthly = "Monthly";
        public const string Quarterly = "Quarterly";
        public const string HalfYearly = "Half Yearly";
        private const string Yearly = "Yearly";


        public PillIntervalPickerCellViewModel()
        {
            Items = new ObservableCollection<string>();
            Items.Add(Monthly);
            Items.Add(Quarterly);
            Items.Add(HalfYearly);
            Items.Add(Yearly);

            SelectionChangedCommand = new Command((index) =>
            {
                if (App.SelectedModel == null)
                    return;

                var selectedIndex = Convert.ToInt32(index);
                CalculateNextDateToBeConsumed(selectedIndex);

                // DEMO  USAGE OF MessagingCenter
                MessagingCenter.Send(this, "pillIntervalChanged");
            });
        }

        static int? CalculateIndex()
        {
            if (App.SelectedModel == null)
                return 0;

            //TODO: THERE'S A BUG - SELECT SERVICE INTERVAL FIRST THEN SELECT LAST SERVICE DATE- FIX LATER
            var duration = App.SelectedModel.NextDateToBeConsumed.Subtract(App.SelectedModel.LastConsumedDate);

            //approx calculations (only for demo)

            if (duration.TotalDays <= 31)
                return 0;
            else if (duration.TotalDays <= 92)
                return 1;
            else if (duration.TotalDays <= 184)
                return 2;
            else if (duration.TotalDays <= 366)
                return 3;
            return 0;
        }

        public int? GetSelectedIndex()
        {
            // Get SelectedIndex makes sense only for existing items
            return CalculateIndex();
        }

        public static void CalculateNextDateToBeConsumed(int? selectedIndex = null)
        {
            if (!selectedIndex.HasValue)
                selectedIndex = CalculateIndex();

            App.SelectedModel.NextDateToBeConsumed = App.SelectedModel.LastConsumedDate;

            if (selectedIndex == 0)
                App.SelectedModel.NextDateToBeConsumed = App.SelectedModel.NextDateToBeConsumed.AddMonths(1);
            if (selectedIndex == 1)
                App.SelectedModel.NextDateToBeConsumed = App.SelectedModel.NextDateToBeConsumed.AddMonths(3);
            if (selectedIndex == 2)
                App.SelectedModel.NextDateToBeConsumed = App.SelectedModel.NextDateToBeConsumed.AddMonths(6);
            if (selectedIndex == 3)
                App.SelectedModel.NextDateToBeConsumed = App.SelectedModel.NextDateToBeConsumed.AddMonths(12);
        }



        private string pickerText = "Pill Interval:";

        public string PickerText
        {
            get { return pickerText; }
            set { pickerText = value; OnPropertyChanged(); }
        }

        private string pickerTitle = "Select Interval";

        public string PickerTitle
        {
            get { return pickerTitle; }
            set { pickerTitle = value; OnPropertyChanged(); }
        }

        private static Command selectionChangedCommand;

        public Command SelectionChangedCommand
        {
            get { return selectionChangedCommand; }
            set { selectionChangedCommand = value; OnPropertyChanged(); }
        }

        private IList<string> items;

        public IList<string> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(); }
        }


    }
}
