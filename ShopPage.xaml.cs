using ClaudiuCojocaruLab7.Models;
using Plugin.LocalNotification;

namespace ClaudiuCojocaruLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        bool confirm = await DisplayAlert(
            "Delete shop",
            "Are you sure?",
            "Yes", "No");

        if (!confirm)
            return;

        await App.Database.DeleteShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is not Shop shop)
        {
            await DisplayAlert("Error", "Shop not loaded.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(shop.Adress))
        {
            await DisplayAlert(
                "Error",
                "Please enter a valid shop address.",
                "OK");
            return;
        }

        IEnumerable<Location> locations;

        try
        {
            locations = await Geocoding.GetLocationsAsync(shop.Adress);
        }
        catch
        {
            await DisplayAlert(
                "Error",
                "Geocoding failed. Check internet connection.",
                "OK");
            return;
        }

        var location = locations?.FirstOrDefault();

        if (location == null)
        {
            await DisplayAlert(
                "Error",
                "Address not found.",
                "OK");
            return;
        }

        var myLocation = new Location(46.7731796289, 23.6213886738);
        var distance = myLocation.CalculateDistance(location, DistanceUnits.Kilometers);

        if (distance < 4)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = shop.Adress,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }

        await Map.OpenAsync(location, new MapLaunchOptions
        {
            Name = shop.ShopName
        });
    }

}