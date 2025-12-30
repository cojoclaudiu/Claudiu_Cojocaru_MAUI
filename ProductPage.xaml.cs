using ClaudiuCojocaruLab7.Models;

namespace ClaudiuCojocaruLab7;

public partial class ProductPage : ContentPage
{
    ShopList sl;

    public ProductPage(ShopList slist)
    {
        InitializeComponent();
        sl = slist;

        BindingContext = new Product();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var product = (Product)BindingContext;
        await App.Database.SaveProductAsync(product);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Product product)
        {
            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Product p)
        {
            var lp = new ListProduct
            {
                ShopListID = sl.ID,
                ProductID = p.ID
            };

            await App.Database.SaveListProductAsync(lp);
            await Navigation.PopAsync();
        }
    }
}