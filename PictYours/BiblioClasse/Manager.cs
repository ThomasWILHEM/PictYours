﻿
namespace BiblioClasse
{
    public class Manager
    {
        public IPersistanceManager Persistance { get;  set; }
        public ManagerUtilisateur ManagerUtilisateur { get; private set; }
        public ManagerPhoto ManagerPhoto { get; private set; }
       

        public Manager(IPersistanceManager persistance)
        {
            Persistance = persistance;
            ManagerUtilisateur = new ManagerUtilisateur();
            ManagerPhoto = new ManagerPhoto();
        }

        public void ChargeDonnees()
        {
            var donnees = Persistance.ChargeDonnees();
            ManagerUtilisateur.ChargeDonnees(donnees.listeUtilisateurs);
            ManagerPhoto.ChargeDonnees(donnees.photosParUtilisateurs, donnees.listeUtilisateursParPhotosAimees);
            Photo.prochainIdentifiant = donnees.prochainIdentifiant;
        }

        public void SauvegardeDonnees()
        {
            var donneesManagerUtilisateur = ManagerUtilisateur.SauvegardeDonnees();
            var donnesManagerPhoto = ManagerPhoto.SauvegardeDonnees();
            Persistance.SauvegardeDonnees(donneesManagerUtilisateur, donnesManagerPhoto.PhotosParUtilisateurs, donnesManagerPhoto.ListeUtilisateursParPhotosAimees, Photo.prochainIdentifiant);
        }
    }
}