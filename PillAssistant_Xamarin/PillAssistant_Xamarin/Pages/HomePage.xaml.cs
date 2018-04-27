using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillAssistant_Xamarin.Data;
using PillAssistant_Xamarin.ViewModels;
using PillAssistant_Xamarin.ViewModels.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PillAssistant_Xamarin.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        HomePageViewModel vm;

        public HomePage()
        {
            InitializeComponent();

            this.BindingContext = vm = new HomePageViewModel();

            listView.ItemSelected += listView_ItemSelected;

        }

        private Action Save()
        {
            return async () =>
            {
                App.SelectedModel = null;
                await Navigation.PushAsync(new EditReminderPage());
            };
        }

        async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.SelectedModel = new ReminderItemDatabase().GetItem((e.SelectedItem as ReminderListItem).Id);
            await Navigation.PushAsync(new EditReminderPage());
        }

        ToolbarItem toolBarItem;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.PopulateReminderList();
            listView.ItemsSource = vm.ReminderList;

            ToolbarItems.Add(toolBarItem = new ToolbarItem("Add", null, Save(), 0, 0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ToolbarItems.Remove(toolBarItem);
        }


    }
}