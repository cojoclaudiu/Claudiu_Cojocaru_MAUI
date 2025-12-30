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
        var slist = (ShopList)BindingContext;

        if (ShopPicker.SelectedItem is Shop selectedShop)
            slist.ShopID = selectedShop.ID;

        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);

        await Navigation.PopAsync();
    }


    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);

        await Shell.Current.GoToAsync("..");
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is not ShopList sl)
            return;

        if (sl.ID == 0)
        {
            sl.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(sl);
        }

        await Navigation.PushAsync(new ProductPage(sl));
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shops = await App.Database.GetShopsAsync();
        ShopPicker.ItemsSource = shops;
        ShopPicker.ItemDisplayBinding = new Binding("ShopDetails");

        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

}