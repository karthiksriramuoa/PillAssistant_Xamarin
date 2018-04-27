﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PillAssistant_Xamarin.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerCellView
    {
        public PickerCellView()
        {
            InitializeComponent();

            picker.SelectedIndexChanged += picker_SelectedIndexChanged;

        }

        public void PopulatePicker()
        {
            var pickerViewModel = (IPickerCellViewModel)BindingContext;
            foreach (var item in pickerViewModel.Items)
            {
                picker.Items.Add(item);
            }
        }

        public void SetPickerSelectedIndex(int pos)
        {
            picker.SetValue(Picker.SelectedIndexProperty, pos);
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pickerViewModel = (IPickerCellViewModel)BindingContext;
            if (pickerViewModel != null && pickerViewModel.SelectionChangedCommand != null)
                pickerViewModel.SelectionChangedCommand.Execute(picker.SelectedIndex);
        }
    }

    public class PickerCell<T> : ViewCell where T : IPickerCellViewModel
    {

        public static readonly BindableProperty PickerCellViewModelProperty =
                BindableProperty.Create<PickerCell<T>, T>(p => p.PickerCellViewModel, Activator.CreateInstance<T>());

        public T PickerCellViewModel
        {
            get { return (T)GetValue(PickerCellViewModelProperty); }
            set { SetValue(PickerCellViewModelProperty, value); }
        }


        PickerCellView vw;
        public PickerCell()
        {
            vw = new PickerCellView();
            vw.BindingContext = PickerCellViewModel;
            vw.PopulatePicker();
            var index = PickerCellViewModel.GetSelectedIndex();
            if (index.HasValue)
                vw.SetPickerSelectedIndex(index.Value);

            View = vw;

        }
    }
}