using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProyectoRecetario
{
    public  enum Categoria
    {
        Postre,
        PlatoFuerte,
        Almuerzo,
        Entrada,
    }
   public class Receta:INotifyPropertyChanged
    {
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value;
                NotifyPropertyChanged();
            }
        }
        private Categoria tipo;

        public Categoria Tipo
        {
            get { return tipo; }
            set { tipo= value;
                NotifyPropertyChanged();
            }
        }
        
        private string ingredientes;

        public string Ingredientes
        {
            get { return ingredientes; }
            set { ingredientes = value;
                NotifyPropertyChanged();
            }
        }
        private string modoPreparacion;

        public string   ModoPreparaciom
        {
            get { return modoPreparacion; }
            set { modoPreparacion = value; }
        }
        private string foto;

        public string   Foto
        {
            get { return foto; }
            set {
                foto = value;

                NotifyPropertyChanged();
            }
        }
        private string tiempo;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        public string Tiempo
        {
            get { return    tiempo; }
            set { tiempo = value; }
        }
       

    }
}
