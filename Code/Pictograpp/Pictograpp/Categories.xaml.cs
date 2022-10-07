using Pictograpp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            if (cate != null)
            {
                if (CanDelete(cate))
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
        }
        private bool CanDelete(MCategorias cate)
        {
            //tendria que hacer una select en pictograma para ver si 
            //existe alguno con esa categoria y en ese caso no se puede eliminar
            //to do: cargar pictograma; fijarse que ande el text to speech de la manera que precisamos
            //en caso de que no, buscar como cargar audio desde el microfono
            //hacer grids segun la cantidad de categorias
            return true;
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
            BtnElegirPicto.IsVisible = true;
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
            await PickerPhotoAsync();
            if (ValidarDatosPicto())
            {
                MPictogramas pic = new MPictogramas
                {
                    NomPicto = TxTNomPicto.Text,
                    TextoPicto = TxTPictoTexto.Text,
                    CodCat = int.Parse(TxTCodCatP.Text),
                    Picto = PhotoPath
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
                if (CanDeleteP(pict))
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
        }

        //control de si se autoriza a eliminar el objeto
        private bool CanDeleteP(MPictogramas pict)
        {
            return true;
        }
        #endregion

        private async void BtnPickImage_Clicked(object sender, EventArgs e)
        {
            await PickerPhotoAsync();
            /*var ima = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title="Elegi un pictograma o imagen"
            });
            var stream = await ima.OpenReadAsync();

            //byte[] ImageByte = new byte[r.Length];
            // r.InputStream.Read(ImageByte, 0, ImageByte.Length);

            //string ImageBase64 = Convert.ToBase64String(ImageByte); 

            // byte[] newImageByte = Convert.FromBase64String(ImageBase64);
            //  ResultImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes)); 
            */
        }

        string PhotoPath;
        string imageBase64;
        async Task PickerPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            //gaurdar archivo
            //var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            Stream stream = await photo.OpenReadAsync();
            /* using (var newStream = File.OpenWrite(newFile))
             {
                 await stream.CopyToAsync(newStream);
             }*/

            //PhotoPath = newFile;

            var result = GetImageStreamAsBytes(stream);
            string imageBase64 = Convert.ToBase64String(result);
            PhotoPath = imageBase64;
            //PhotoPath = result;
            //string imageBase64 = Convert.ToBase64String(result);
           // PhotoPath = imageBase64;

        }
        /*
        public byte[] GetImagesBytes(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            byte[] bytes = ms.ToArray();
            return bytes;
        }
        */
        public byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void BtnBackupBaseDeDatos_Clicked(object sender, EventArgs e)
        {

                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");
                var bytes = File.ReadAllBytes(path);
                var fileCopyName = string.Format("/sdcard/Database_{0:dd-MM-yyyy_HH-mm-ss-tt}.db", DateTime.Now);
                File.WriteAllBytes(fileCopyName, bytes);
            
        }

        private void BtnElegirPicto_Clicked(object sender, EventArgs e)
        {
            
            byte[] bytes = System.Convert.FromBase64String(imageBase64);
            ResultPicto.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        /*public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            byte[] result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                result = ms.ToArray();
            }
            return result;
        }
        */
        /*
        public static void SaveByteArrayToFileWithFileStream(byte[] data, string filePath)
        {
            using var stream = File.Create(filePath);
            stream.Write(data, 0, data.Length);
        }
        */
        /*private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }
        */
    }
}