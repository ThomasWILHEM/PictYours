using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class Utilisateur
    {
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom 
        {
            get => nom;
            set
            {
                if (value != null)
                    nom = value;
            }
        }
        private string nom;

        /// <summary>
        /// Prenom de l'utilisateur
        /// </summary>
        public string Prenom
        {
            get => prenom;
            set
            {
                if (value != null)
                    prenom = value;
            }
        }
        private string prenom;

        /// <summary>
        /// Pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get => pseudo;
            set
            {
                if (value != null)
                    pseudo = value;
            }
        }
        private string pseudo;

        /// <summary>
        /// Date de naissance de l'utilisateur
        /// </summary>
        public DateTime DateDeNaissance
        {
            get
            {
                return dateDeNaissance;
            }
            set
            {
                dateDeNaissance = value;
            }
        }
        private DateTime dateDeNaissance;

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public string MotDePasse { get; set; }

        /// <summary>
        /// Si le booleen est vrai alors l'utilisateur est connecté si non faux
        /// </summary>
        public bool EstConnecte { get; set; }

        /// <summary>
        /// Liste des photos de l'utilisateur
        /// </summary>
        public List<Photo> MesPhotos { get; private set; }

        /// <summary>
        /// Liste des photos aimées de l'utilisateur
        /// </summary>
        public List<Photo> PhotosAimees { get; private set; }

        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="pseudo"></param>
        /// <param name="dateDeNaissance"></param>
        /// <param name="motDePasse"></param>
        public Utilisateur(string nom, string prenom, string pseudo, DateTime dateDeNaissance, string motDePasse)
        {
            Nom = nom;
            Prenom = prenom;
            Pseudo = $"@{pseudo}";
            DateDeNaissance = dateDeNaissance;
            MotDePasse = motDePasse;
            MesPhotos = new List<Photo>();
            PhotosAimees = new List<Photo>();
        }

        /// <summary>
        /// Ajoute une photo passee en paramètre dans la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        /// <returns>Retourne vrai si tout s'est bien passé si non faux</returns>
        public bool AjouterPhoto(Photo photo)
        {
            if (photo != null)
            {
                MesPhotos.Add(photo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Supprime une photo passee en paramètre de la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        /// <returns>Retourne vrai si tout s'est bien passé si non faux</returns>
        public bool SupprimerPhoto(Photo photo)
        {
            if (photo != null)
                return MesPhotos.Remove(photo);
            return false;
        }

        /// <summary>
        /// Ajoute une photo dans la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à ajouter</param>
        public void AimerPhoto(Photo photo)
        {
            PhotosAimees.Add(photo);
        }

        /// <summary>
        /// Supprime une photo de la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void NePlusAimerPhoto(Photo photo)
        {
            PhotosAimees.Remove(photo);
        }
    }
}
