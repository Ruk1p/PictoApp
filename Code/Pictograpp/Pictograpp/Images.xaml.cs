using Pictograpp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pictograpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Images : ContentPage
    {
        IEnumerable<Locale> locales;

        public Images()
        {
            InitializeComponent();
        }


        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await Navigation.PopAsync();
        }

        private async void Mama_Clicked(object sender, EventArgs e)
        {

            await TextToSpeech.SpeakAsync("Mama");

        }
        private async void Papa_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync("Papá");
        }
        private async void Dormir_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync("quiero dormir");
        }
        private async void TomarAgua_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync("Quiero tomar agua");
        }

        private async void TengoCalor_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync("Tengo Calor");
        }
        private async void Pintar_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync("Quiero Pintar");

        }
    }
}