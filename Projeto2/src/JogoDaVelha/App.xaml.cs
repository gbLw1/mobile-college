namespace JogoDaVelha;

public partial class App : Application
{
    public App()
    {
        Current.UserAppTheme = AppTheme.Light;

        InitializeComponent();

        MainPage = new MainPage();
    }
}
