using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PillAssistant_Xamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditReminderPage
    {

        ViewModels.Pages.EditReminderPageViewModel vm;
        public EditReminderPage()
        {
            InitializeComponent();

            vm = new ViewModels.Pages.EditReminderPageViewModel();
            // Sometimes Commands in ViewModels will require to navigate.
            vm.Navigation = Navigation;

            this.BindingContext = vm;

            SetBinding(Page.TitleProperty, new Binding(ViewModels.Pages.PageViewModelBase.TitlePropertyName));
            SetBinding(Page.IconProperty, new Binding(ViewModels.Pages.PageViewModelBase.IconPropertyName));

        }

        private Action Save()
        {
            return async () => {
                var result = await vm.Save();
                if (!result)
                    await DisplayAlert("Error", "Name and Pill Identification Number are required.", "OK", null);
                else
                    await Navigation.PopAsync();

            };
        }

        ToolbarItem toolbarItem;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToolbarItems.Add(toolbarItem = new ToolbarItem("Save", null, Save(), 0, 0));

            vm.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ToolbarItems.Remove(toolbarItem);
        }


    }
}