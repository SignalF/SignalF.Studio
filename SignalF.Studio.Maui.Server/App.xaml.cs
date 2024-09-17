namespace SignalF.Studio.Maui.Server
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = mainPage;
        }
    }
}
