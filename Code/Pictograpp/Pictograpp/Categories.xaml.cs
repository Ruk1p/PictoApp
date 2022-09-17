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
            //Esto hay que cambiarlo de lugar cuando damasco haga los botones
            MostrarDatosCat();
            MostrarDatosPicto();
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Images());
        }

        #region Categorias
        private void LimpiarCat()
        {
            TxtCodCat.Text = "";
            TxTNomCat.Text = "";
        }

        public bool ValidarDatosCat()
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

        public async void MostrarDatosCat()
        {
            //mostrar la base de datos despues de registrar la categoria
            var CatList = await App.SQLiteDB.GetCatAsync();
            if (CatList != null)
            {
                LstCat.ItemsSource = CatList;
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
                var cat = await App.SQLiteDB.GetCatByCodAsync(obj.CodCat);
                if (cat != null)
                {
                    TxtCodCat.Text = cat.CodCat.ToString();
                    TxTNomCat.Text = cat.NomCat;
                }
            }
        }

        /// <summary>
        /// Registar categorias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnRegistrarCat_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatosCat())
            {
                MCategorias cat = new MCategorias
                {
                    NomCat = TxTNomCat.Text
                };
                await App.SQLiteDB.SaveCatAsync(cat);
                await DisplayAlert("Registro", "Se guardo de manera exitosa la categoria", "Ok");
                LimpiarCat();
                MostrarDatosCat();
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
                await DisplayAlert("Modificación", "Se edito de manera exitosa la categoria", "Ok");
                TxtCodCat.IsVisible = false;
                BtnActualizarCat.IsVisible = false;
                BtnRegistrarCat.IsVisible = true;
                LimpiarCat();
                MostrarDatosCat();
            }
        }

        private async void BtnEliminarCat_Clicked(object sender, EventArgs e)
        {
            var cate = await App.SQLiteDB.GetCatByCodAsync(Convert.ToInt32(TxtCodCat.Text));
            if(cate != null)
            {
                await App.SQLiteDB.DeleteCatAsync(cate);
                await DisplayAlert("Eliminado", "La categoria a sido eliminada", "Ok");
                TxtCodCat.IsVisible = false;
                BtnActualizarCat.IsVisible = false;
                BtnEliminarCat.IsVisible = false;
                BtnRegistrarCat.IsVisible = true;
                LimpiarCat();
                MostrarDatosCat();
            }
        }
        #endregion

        #region Pictogramas
        private void LimpiarPicto()
        {
            TxtCodPicto.Text = "";
            TxTNomPicto.Text = "";
            TxTPictoTexto.Text = "";
            TxTCodCatP.Text = "";
        }

        public bool ValidarDatosPicto()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(TxTNomPicto.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(TxTPictoTexto.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async void MostrarDatosPicto()
        {
            //mostrar la base de datos despues de registrar la categoria
            var PictoList = await App.SQLiteDB.GetPictoAsync();
            if (PictoList != null)
            {
                LstPicto.ItemsSource = PictoList;
            }
        }

        private async void LstPicto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (MPictogramas)e.SelectedItem;
            BtnRegistrarPicto.IsVisible = false;
            TxTNomPicto.IsVisible = true;
            BtnActualizarPicto.IsVisible = true;
            BtnEliminarPicto.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.CodPicto.ToString()))
            {
                var picto = await App.SQLiteDB.GetPictoByCodAsync(obj.CodPicto);
                if (picto != null)
                {
                    TxtCodPicto.Text = picto.CodPicto.ToString();
                    TxTNomPicto.Text = picto.NomPicto;
                    TxTPictoTexto.Text = picto.TextoPicto;
                    TxTCodCatP.Text = picto.CodCat.ToString();
                }
            }
        }

        private async void BtnRegistrarPicto_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatosPicto())
            {
                MPictogramas pic = new MPictogramas
                {
                    NomPicto = TxTNomPicto.Text,
                    TextoPicto = TxTPictoTexto.Text,
                    CodCat = int.Parse(TxTCodCatP.Text)
                };
                await App.SQLiteDB.SavePictoAsync(pic);
                await DisplayAlert("Registro", "Se guardo de manera exitosa el pictograma", "Ok");
                LimpiarPicto();
                MostrarDatosPicto();
            }
            else
            {
                await DisplayAlert("Error", "Ingrese los datos de manera correcta", "Ok");
            }
        }

        private async void BtnActualizarPicto_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCodPicto.Text))
            {
                MPictogramas pictograma = new MPictogramas()
                {
                    CodPicto = Convert.ToInt32(TxtCodPicto.Text),
                    NomPicto = TxTNomPicto.Text,
                    TextoPicto = TxTPictoTexto.Text,
                    CodCat = Convert.ToInt32(TxTCodCatP.Text),
                };
                await App.SQLiteDB.SavePictoAsync(pictograma);
                await DisplayAlert("Modificación", "Se edito de manera exitosa el pictograma", "Ok");
                TxtCodPicto.IsVisible = false;
                BtnActualizarPicto.IsVisible = false;
                BtnRegistrarPicto.IsVisible = true;
                LimpiarPicto();
                MostrarDatosPicto();
            }
        }

        private async void BtnEliminarPicto_Clicked(object sender, EventArgs e)
        {
            var pict = await App.SQLiteDB.GetPictoByCodAsync(Convert.ToInt32(TxtCodPicto.Text));
            if (pict != null)
            {
                await App.SQLiteDB.DeletePictoAsync(pict);
                await DisplayAlert("Eliminado", "El pictograma a sido eliminada", "Ok");
                TxtCodPicto.IsVisible = false;
                BtnActualizarPicto.IsVisible = false;
                BtnEliminarPicto.IsVisible = false;
                BtnRegistrarPicto.IsVisible = true;
                LimpiarPicto();
                MostrarDatosPicto();
            }
        }

        #endregion
    }
}