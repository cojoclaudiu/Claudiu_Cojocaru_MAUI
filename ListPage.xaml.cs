using ClaudiuCojocaruLab7.Models;

namespace ClaudiuCojocaruLab7;

[QueryProperty(nameof(ShopList), "ShopList")]
public partial class ListPage : ContentPage
{
    public ShopList ShopList
    {
        get => BindingContext as ShopList;
        set => BindingContext = value;
    }

    public ListPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is not ShopList slist)
            return;

        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);

        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);

        await Shell.Current.GoToAsync("..");
    }
}