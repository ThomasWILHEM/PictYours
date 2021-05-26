﻿using AppWpf;
using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour ProfilUtilisateur.xaml
    /// </summary>
    public partial class ProfilUtilisateur : UserControl
    {
        public Manager LeManager => (App.Current as App).LeManager;

        public static readonly DependencyProperty UtilisateurProperty
            = DependencyProperty.Register(nameof(Utilisateur), typeof(Utilisateur), typeof(ProfilUtilisateur));



        public Utilisateur Utilisateur
        {
            get => GetValue(UtilisateurProperty) as Utilisateur;
            set => SetValue(UtilisateurProperty, value);
        }

        public ProfilUtilisateur()
        {
            InitializeComponent();
        }

        private void ModifButton_Click(object sender, RoutedEventArgs e)
        {
            //if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
            //{
            //    UCModifProfil.PrenomBox.Text = amateur.Prenom;

            //    UCModifProfil.DateDeNaissanceBox.SelectedDate = amateur.DateDeNaissance;
            //}
            //else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial commercial)
            //{
            //    UCModifProfil.SiteBox.Text = commercial.SiteWeb;
            //}
            //UCModifProfil.NomBox.Text = LeManager.ManagerUtilisateur.UtilisateurActuel.Nom;
            //UCModifProfil.DescBox.Text = LeManager.ManagerUtilisateur.UtilisateurActuel.Description;
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                UCModifProfil.UCCommercial.Visibility = Visibility.Visible;
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur)
            {
                UCModifProfil.UCAmateur.Visibility = Visibility.Visible;
            }


        }

        //private void ListeBoxPhotos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count > 0)
        //        LeManager.ManagerPhoto.PhotoSelectionne = e.AddedItems[0] as BiblioClasse.Photo;
        //}
    }
}
