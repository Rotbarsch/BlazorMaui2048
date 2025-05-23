namespace Twothousandfortyeight.Maui;

public partial class MainPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = _viewModel;

        this.Loaded += (sender, args) =>
        {
            _viewModel.RegisterKeys(this.Handler);
        };
    }

}