using Pictograpp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pictograpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Images : ContentPage
    {
        public Images()
        {
            InitializeComponent();
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await Navigation.PopAsync();
        }
    }
}