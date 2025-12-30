namespace ClaudiuCojocaruLab7;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
    }
}