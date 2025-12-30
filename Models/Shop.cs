using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ClaudiuCojocaruLab7.Models;

public class Shop
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string ShopName { get; set; }
    public string Adress { get; set; }

    public string ShopDetails => ShopName + "\n" + Adress;

    [OneToMany]
    public List<ShopList> ShopLists { get; set; }
}