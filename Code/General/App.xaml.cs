using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pictograpp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Categories());
            //MainPage = new Categories();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
