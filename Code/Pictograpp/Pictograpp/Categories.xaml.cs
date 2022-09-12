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
            MostrarDatos();
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Images());
        }

        private void LimpiarCat()
        {
            TxtCodCat.Text = "";
            TxTNomCat.Text = "";
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

        public async void MostrarDatos()
        {
            //mostrar la base de datos despues de registrar la categoria
            var CatList = await App.SQLiteDB.GetCategoriasAsync();
            if (CatList != null)
            {
                LstCat.ItemsSource = CatList;
            }
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
                MCategorias cat = new MCategorias
                {
                    NomCat = TxTNomCat.Text
                };
                await App.SQLiteDB.SaveCatAsync(cat);
                LimpiarCat();
                await DisplayAlert("Registro", "Se guardo de manera exitosa la categoria", "Ok");
                MostrarDatos();
            }
            else
            {
                await DisplayAlert("Error", "Ingrese los datos", "Ok");
            }
        }

        private async void BtnActualizarCat_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCodCat.Text))
            {
                MCategorias categorias = new MCategorias()
                {
                    CodCat = Convert.ToInt32(TxtCodCat.Text),
                    NomCat = TxTNomCat.Text
                };
                await App.SQLiteDB.SaveCatAsync(categorias);
                LimpiarCat();
                await DisplayAlert("Modificación", "Se edito de manera exitosa la categoria", "Ok");
                TxtCodCat.IsVisible = false;
                BtnActualizarCat.IsVisible = false;
                BtnRegistrarCat.IsVisible = true;
                MostrarDatos();
            }
        }

        private async void LstCat_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (MCategorias)e.SelectedItem;
            BtnRegistrarCat.IsVisible = false;
            TxTNomCat.IsVisible = true;
            BtnActualizarCat.IsVisible = true;
            BtnEliminarCat.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.CodCat.ToString()))
            {
                var cat = await App.SQLiteDB.GetCategoriasByCodAsync(obj.CodCat);
                if (cat != null)
                {
                    TxtCodCat.Text = cat.CodCat.ToString();
                    TxTNomCat.Text = cat.NomCat;
                }
            }
        }

        private async void BtnEliminarCat_Clicked(object sender, EventArgs e)
        {
            var cate = await App.SQLiteDB.GetCategoriasByCodAsync(Convert.ToInt32(TxtCodCat.Text));
            if(cate != null)
            {
                await App.SQLiteDB.DeleteCatAsync(cate);
                await DisplayAlert("Eliminado", "La categoria a sido eliminada", "Ok");
                LimpiarCat();
                MostrarDatos();
                TxtCodCat.IsVisible = false;
                BtnActualizarCat.IsVisible = false;
                BtnEliminarCat.IsVisible = false;
                BtnRegistrarCat.IsVisible = true;
            }
        }
    }
}