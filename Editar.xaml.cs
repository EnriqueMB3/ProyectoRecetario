using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace ProyectoRecetario
{
    /// <summary>
    /// Se crea una propiedad de la clase receta que se trae los datos que están seleccionados en el index del listbox que está en el main
    /// Y a través del evento que Window_Loaded nos los carga
    /// </summary>
    public partial class Editar : Window
    {
        public Editar()
        {
            InitializeComponent();
            cmbCategoria.ItemsSource = Enum.GetValues(typeof(Categoria));
        }
        public Receta receta { get; set; }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = receta.Nombre;
            cmbCategoria.SelectedValue = receta.Tipo;
            txtIngredientes.Text = receta.Ingredientes;
            txtModo.Text = receta.ModoPreparaciom;
            txtTiempo.Text = receta.Tiempo;
            absName = receta.Foto;
            imgPhoto.Source = new BitmapImage(new Uri(AppContext.BaseDirectory + "/imagenes/" + receta.Foto));
        }
        string absName;
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            receta.Nombre = txtNombre.Text;
            receta.Tipo = ((Categoria)cmbCategoria.SelectedValue);
            receta.Ingredientes = txtIngredientes.Text;
            receta.ModoPreparaciom = txtModo.Text;
            receta.Tiempo = txtTiempo.Text;
            if (absName != "")
            {
               receta.Foto = absName;  
            }
            receta.Foto = absName;
            Close();
        }

        private void AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "Archivos de imágen (.jpg)|*.jpg|All Files (*.*)|*.*";
                fd.Multiselect = false;
                bool? checarOK = fd.ShowDialog();
                if (checarOK == true)
                {
                    string archivoOrigen = fd.FileName;
                    absName = fd.SafeFileName;
                    string rutaDestino = AppDomain.CurrentDomain.BaseDirectory + "\\imagenes\\";
                    string archivoDestino = System.IO.Path.Combine(rutaDestino, fd.SafeFileName);
                    if (!Directory.Exists(rutaDestino))
                    {
                        Directory.CreateDirectory(rutaDestino);
                    }
                    File.Copy(archivoOrigen, archivoDestino, true);
                    imgPhoto.Source = new BitmapImage(new Uri(archivoDestino));
                }
            }
            catch (Exception ed)
            {
                MessageBox.Show(ed.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

