using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour Photo.xaml
    /// </summary>
    public partial class Photo : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty UtilisateurProperty
            = DependencyProperty.Register(nameof(BiblioClasse.Photo), typeof(BiblioClasse.Photo), typeof(Photo));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public BiblioClasse.Photo LaPhoto
        {
            get => GetValue(UtilisateurProperty) as BiblioClasse.Photo;
            set
            {
                SetValue(UtilisateurProperty, value);
                OnPropertyChanged();
            }
        }

        public Photo()
        {
            InitializeComponent();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
