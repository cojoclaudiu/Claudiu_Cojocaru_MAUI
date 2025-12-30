namespace ClaudiuCojocaruLab7.Models;
using SQLite;

public class ShopList
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [MaxLength(250), Unique]
    public string Description { get; set; }
    public DateTime Date { get; set; }
}