using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaudiuCojocaruLab7.Models;

namespace ClaudiuCojocaruLab7;

public partial class ListEntryPage : ContentPage
{
    public ListEntryPage()
    {
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopListsAsync();
    }
    async void OnShopListAddedClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListPage),
            new Dictionary<string, object>
            {
                ["ShopList"] = new ShopList()
            });
    }
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Shell.Current.GoToAsync(nameof(ListPage),
                new Dictionary<string, object>
                {
                    ["ShopList"] = e.SelectedItem
                });

            ((ListView)sender).SelectedItem = null;
        }
    }
}