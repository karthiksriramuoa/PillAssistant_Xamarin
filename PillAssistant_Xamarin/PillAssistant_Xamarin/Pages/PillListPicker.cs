using System.Collections.Generic;
using Xamarin.Forms;

namespace PillAssistant_Xamarin.Pages
{
    public class Pill
    {
        public string Type { get; set; }
        public string Photo { get; set; }

        public string Detail { get; set; }

    }
    public class PillListPicker : ContentPage
    {
        public string SelectedPill { get; set; }
        public PillListPicker()
        {
            var PillTypes = new List<Pill>()
            {
                new Pill(){ Type = "Aspirin", Photo = "Car.png" , Detail = "Cures Headache"},
                new Pill(){ Type = "Paracetamol", Photo = "Bike.png", Detail = "Cures Fever"},
                new Pill(){ Type = "Dygene", Photo = "Other.png", Detail= "Cures Indigestion"},
            };


            var listView = new ListView()
            {

                ItemsSource = PillTypes,

                ItemTemplate = new DataTemplate(() =>
                {

                    Label typeLabel = new Label();
                    typeLabel.SetBinding(Label.TextProperty, "Type");

                    Image image = new Image();
                    image.SetBinding(Image.SourceProperty, "Photo");

                    Label detailLabel = new Label();
                    detailLabel.SetBinding(Label.TextProperty,
                        new Binding("Detail", BindingMode.OneWay,
                                    null, null, "Pill {0}"));

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                image,
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 0,
                                    Children =
                                    {
                                        typeLabel,
                                        detailLabel
                                    }
                                }
                            }
                        }
                    };

                })

            };

            listView.ItemTapped += listView_ItemTapped;

            Label header = new Label
            {
                Text = "Choose Pill",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };
        }

        async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            SelectedPill = (e.Item as Pill).Type;
            await Navigation.PopAsync();
        }
    }
}
