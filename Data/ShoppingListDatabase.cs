using SQLite;
using ClaudiuCojocaruLab7.Models;

namespace ClaudiuCojocaruLab7.Data;

public class ShoppingListDatabase
{
    readonly SQLiteAsyncConnection _database;

    public ShoppingListDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<ShopList>().Wait();
        _database.CreateTableAsync<Product>().Wait();
        _database.CreateTableAsync<ListProduct>().Wait();
    }
    
    public Task<List<ShopList>> GetShopListsAsync()
    {
        return _database.Table<ShopList>().ToListAsync();
    }

    public Task<int> SaveShopListAsync(ShopList slist)
    {
        if (slist.ID != 0)
        {
            return _database.UpdateAsync(slist);
        }
        else
        {
            return _database.InsertAsync(slist);
        }
    }

    public Task<int> DeleteShopListAsync(ShopList slist)
    {
        return _database.DeleteAsync(slist);
    }
    
    public Task<int> SaveProductAsync(Product product)
    {
        if (product.ID != 0)
        {
            return _database.UpdateAsync(product);
        }
        else
        {
            return _database.InsertAsync(product);
        }
    }

    public Task<int> DeleteProductAsync(Product product)
    {
        return _database.DeleteAsync(product);
    }

    public Task<List<Product>> GetProductsAsync()
    {
        return _database.Table<Product>().ToListAsync();
    }
    
    public Task<int> SaveListProductAsync(ListProduct listp)
    {
        if (listp.ID != 0)
        {
            return _database.UpdateAsync(listp);
        }
        else
        {
            return _database.InsertAsync(listp);
        }
    }

    public Task<List<Product>> GetListProductsAsync(int shoplistid)
    {
        return _database.QueryAsync<Product>(
            "SELECT P.ID, P.Description FROM Product P " +
            "INNER JOIN ListProduct LP ON P.ID = LP.ProductID " +
            "WHERE LP.ShopListID = ?",
            shoplistid);
    }
}
