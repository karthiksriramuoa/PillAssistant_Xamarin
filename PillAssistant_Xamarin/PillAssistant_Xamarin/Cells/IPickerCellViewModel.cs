﻿using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace PillAssistant_Xamarin.Cells
{
    public interface IPickerCellViewModel : INotifyPropertyChanged
    {
        string PickerText { get; set; }

        string PickerTitle { get; set; }

        Command SelectionChangedCommand { get; set; }

        IList<string> Items { get; set; }

        int? GetSelectedIndex();
    }

}
