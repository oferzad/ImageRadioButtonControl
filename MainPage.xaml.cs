using Tests.ViewModel;
namespace Tests
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            BindingContext = new MainPageViewModel();
            InitializeComponent();
        }

        
    }

}
