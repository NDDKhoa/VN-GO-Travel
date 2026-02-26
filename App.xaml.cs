using MauiApp1.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App(MapPage mapPage)
        {
            InitializeComponent();
            MainPage = mapPage;
        }
    }

}