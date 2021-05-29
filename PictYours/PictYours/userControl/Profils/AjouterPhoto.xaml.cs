﻿using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace PictYours.userControl.Profils
{
    /// <summary>
    /// Logique d'interaction pour AjouterPhoto.xaml
    /// </summary>
    public partial class AjouterPhoto : UserControl
    {

        public Manager LeManager => (App.Current as App).LeManager;

        public AjouterPhoto()
        {
            InitializeComponent();
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2.5));
        }

        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }

        private void PosterButton_Click(object sender, RoutedEventArgs e)
        {
            if (photoAPoster.ImageSource == null)
            {
                AfficherDansSnackbar("Veuillez sélectionnez une photo");
                Debug.WriteLine("L'image est nulle");
                return;
            }
            if (DescPhoto.Text == string.Empty)
            {
                AfficherDansSnackbar("Veuillez saisir une description pour la photo");
                Debug.WriteLine("La description de la photo est nulle");
                return;
            }
            if (LieuPhoto.Text == string.Empty)
            {
                AfficherDansSnackbar("Veuillez saisir un lieu pour la photo");
                Debug.WriteLine("Le lieu de la photo est nul");
                return;
            }
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new BiblioClasse.Photo(
                new FileInfo(filename).Extension,
                DescPhoto.Text,
                LieuPhoto.Text,
                LeManager.ManagerUtilisateur.UtilisateurActuel,
                (ECategorie)CategorieBox.SelectedItem)
                );
            //Récupere la photo posté
            BiblioClasse.Photo photo = LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.First();
            //Enregistre dans le répertoire des images avec son identifiant
            GestionImage.EnregistrerImage(filename, photo.CheminPhoto, GestionImage.TypeEnregistrement.Images,true);
            ReinitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            ReinitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private string filename;

        private void ParcourirButton_Click(object sender, RoutedEventArgs e)
        {
            filename = GestionImage.ChoisirImage();
            if (filename != null)
            {
                photoAPoster.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }

        private void ReinitialiserChamps()
        {
            photoAPoster.ImageSource = null;
            CategorieBox.SelectedIndex = 0;
            DescPhoto.Clear();
            LieuPhoto.Clear();
        }
    }
}
