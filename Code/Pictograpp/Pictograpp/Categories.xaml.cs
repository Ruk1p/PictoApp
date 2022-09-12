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
    public partial class Categories : ContentPage
    {
        public Categories()
        {
            InitializeComponent();
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Images());
        }

        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(TxTNomCat.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        /// <summary>
        /// Registar categorias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnRegistrarCat_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Categorias cat = new Categorias
                {
                    NomCat = TxTNomCat.Text
                };
                await App.SQLiteDB.SaveCatAsync(cat);
                TxTNomCat.Text = "";
                await DisplayAlert("Registro", "Se guardo de manera exitosa la categoria", "Ok");
                var CatList= await App.SQLiteDB.GetCategoriasAsync();
                if (CatList != null)
                {
                    LstCat.ItemsSource = CatList;
                }
            }
            else
            {
                await DisplayAlert("Error", "Ingrese los datos", "Ok");
            }
        }

    }
}