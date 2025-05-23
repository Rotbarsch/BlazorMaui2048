namespace Twothousandfortyeight.Maui;

public partial class MainPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = _viewModel;

        // As late as possible to ensure all necessary services are ready on all targets
        this.Loaded += (sender, args) =>
        {
            _viewModel.RegisterKeys(this.Handler);
        };
    }
}