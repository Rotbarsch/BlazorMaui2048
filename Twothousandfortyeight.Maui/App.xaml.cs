using Twothousandfortyeight.Game.Services;
using Twothousandfortyeight.Maui.KeyHandler;

namespace Twothousandfortyeight.Maui;

public partial class App : Application
{
    public App(IGameService service, IKeyHandler keyHandler)
    {
        InitializeComponent();

        service.Reset();
        MainPage = new MainPage(service, keyHandler);
    }
}