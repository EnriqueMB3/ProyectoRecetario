using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ProyectoRecetario
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog fd = new OpenFileDialog();
        ClaseDNeg neg = new ClaseDNeg();
        public MainWindow()
        {
            InitializeComponent();
            ClaseDNegLista = neg;
            cmbCategoria.ItemsSource = Enum.GetValues(typeof(Categoria));
        }
        private ClaseDNeg claseDNegLista;

        public ClaseDNeg ClaseDNegLista
        {
            get { return claseDNegLista; }
            set { claseDNegLista = value; }
        }
        string absName;
        string rutaDestino;
        string archivoDestino;
        string archivoOrigen;
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                neg.AgregarReceta(txtNombre.Text, ((Categoria)(cmbCategoria.SelectedItem)), txtIngredientes.Text, txtModo.Text, absName, txtTimes.Text);
                lstRecetas.ItemsSource = ClaseDNegLista.ListaReceta;
                File.Copy(archivoOrigen, archivoDestino, true);
                imgPhoto.Source = new BitmapImage(new Uri(archivoDestino));
                txtNombre.Clear();
                txtIngredientes.Clear();
                txtTimes.Clear();
                txtModo.Clear();
                imgPhoto.Source = null;
                expAdd.IsExpanded = false;
            }
            catch (Exception ed)
            {
                MessageBox.Show(ed.Message);
            }
        }
        private void AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fd.Filter = "Archivos de imágen (.jpg)|*.jpg|All Files (*.*)|*.*";
                fd.Multiselect = false;
                bool? checarOK = fd.ShowDialog();
                if (checarOK == true)
                {
                    archivoOrigen = fd.FileName;
                    absName = fd.SafeFileName;
                    rutaDestino = AppDomain.CurrentDomain.BaseDirectory + "\\imagenes\\";
                    archivoDestino = System.IO.Path.Combine(rutaDestino, fd.SafeFileName);
                    if (!Directory.Exists(rutaDestino))
                    {
                        Directory.CreateDirectory(rutaDestino);
                    }
                    File.Copy(archivoOrigen, archivoDestino, true);
                    imgPhoto.Source = new BitmapImage(new Uri(fd.FileName));
                }
            }
            catch (IOException ed)
            {
                MessageBox.Show(ed.Message + " Pruebe cargando otra imágen", "Error al cargar la imagen", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClaseDNegLista = neg;
            neg.Cargar();
            lstRecetas.ItemsSource = ClaseDNegLista.ListaReceta;
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedIndex != -1)
                claseDNegLista.EliminarReceta((Receta)lstRecetas.SelectedItem);
        }

        private void btnAdelande_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedIndex != -1)
            {
                if (lstRecetas.SelectedIndex == claseDNegLista.ListaReceta.Count - 1)
                {
                    lstRecetas.SelectedIndex = -1;
                }
                lstRecetas.SelectedIndex++;
            }
            
        }
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedIndex != -1)
            {
                if (lstRecetas.SelectedIndex > 0)
                {
                    lstRecetas.SelectedIndex--;
                }
                else
                {
                    lstRecetas.SelectedIndex = claseDNegLista.ListaReceta.Count - 1;
                }
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (lstRecetas.SelectedIndex != -1)
            {
                Editar editar = new Editar();
                editar.receta = (Receta)lstRecetas.SelectedItem;
                editar.ShowDialog();
            }
        }

        private void btnExpandir_Click(object sender, RoutedEventArgs e)
        {
            expAdd.IsExpanded = true;
        }

        private void textBuscador_GotFocus(object sender, RoutedEventArgs e)
        {
            textBuscador.Clear();
            textBuscador.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void textBuscador_LostFocus(object sender, RoutedEventArgs e)
        {
            textBuscador.Text = "Buscar";
            textBuscador.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void textBuscador_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
        }

        private void textBuscador_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            lstRecetas.ItemsSource = ClaseDNegLista.ListaReceta;
        }

        private void textBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            lstRecetas.ItemsSource = neg.Filtro(textBuscador.Text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (claseDNegLista.ListaReceta!=null)
            {
                neg.Guardar();
            }
        }
    }
}
