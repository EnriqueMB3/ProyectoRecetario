using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ProyectoRecetario
{
    /// <summary>
    /// Se crea un ObservableCollection para los objetos receta que se van a almacenar
    /// </summary>
    public class ClaseDNeg
    {
        ObservableCollection<Receta> listaRecet;

        public ObservableCollection<Receta> ListaReceta
        {
            get { return listaRecet; }
            set { listaRecet = value; }
        }
        public void AgregarReceta(Receta receta)
        {
            if (listaRecet == null)
                listaRecet = new ObservableCollection<Receta>();
            listaRecet.Add(receta);
            Guardar();
        }
        public void AgregarReceta(string nombre, Categoria categoria, string ingredientes, string modo, string img, string time)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(categoria.ToString()) || string.IsNullOrWhiteSpace(ingredientes) || string.IsNullOrWhiteSpace(modo) || img == null)
            {
                throw new ApplicationException("Complete la información obligatoria");
            }
            Receta receta = new Receta
            {
                Nombre = nombre,
                Tiempo = time,
                Tipo = categoria,
                Ingredientes = ingredientes,
                ModoPreparaciom = modo,
                Foto = img
            };
            AgregarReceta(receta);
        }
        public void EliminarReceta(Receta receta)
        {
            listaRecet.Remove(receta);
        }
        /// <summary>
        /// Guardar con jsno, bitformatter, o xml. Son formatos de texto para el intercambio de datos
        /// Cuando guardes te podrás dar cuenta la diferencía entre los tres entrando al archivo, 
        /// puedes hacer la prueba con xml sólo cambía "JsonSerializer" por "XmlSerializer" y agrga el using System.Xml.Serialization
        /// A diferencia de Json, el formato de xml es con <> (Etiquetas)
        /// </summary>
        public void Guardar()
        {
            if (listaRecet!=null)
            {
                TextWriter writter = File.CreateText("recetas.json");
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writter, listaRecet, typeof(ObservableCollection<Receta>));
                writter.Close();
            }
        }
        public void Cargar()
        {
            if (File.Exists("recetas.json"))
            {
                TextReader archivo = File.OpenText("recetas.json");
                JsonSerializer json = new JsonSerializer();
                listaRecet = (ObservableCollection<Receta>)json.Deserialize(archivo, typeof(ObservableCollection<Receta>));
                archivo.Close();
            }
        }
        public ObservableCollection<Receta> Filtro(string category)
        {
            ObservableCollection<Receta> d = new ObservableCollection<Receta>(from p in listaRecet where (p.Tipo.ToString().StartsWith(category)) select p);
            return d;
        }

    }
}
