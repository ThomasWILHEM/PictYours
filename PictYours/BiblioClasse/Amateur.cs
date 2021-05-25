using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BiblioClasse
{
    public class Amateur : UtilisateurPrive, IEquatable<Amateur>
    {
        /// <summary>
        /// Prenom de l'Amateur
        /// </summary>
        public string Prenom
        {
            get => prenom;
            internal set
            {
                if (value != null && value!=prenom)
                {
                    prenom = value;
                    OnPropertyChanged();
                }
            }
        }
        private string prenom;

        /// <summary>
        /// Date de naissance de l'Amateur
        /// </summary>
        public DateTime DateDeNaissance
        {
            get => dateDeNaissance;
            set
            {
                if (value != dateDeNaissance)
                {
                    dateDeNaissance = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime dateDeNaissance;

        /// <summary>
        /// Liste des photos aimées de l'Amateur 
        /// </summary>
        private List<Photo> photosAimees = new List<Photo>();
        /// <summary>
        /// ReadOnlyCollection de photos aimées de l'Amateur
        /// </summary>
        public ReadOnlyCollection<Photo> PhotosAimees { get; }

        /// <summary>
        /// Constructeur d'un utilisateur Amateur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prenom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil de l'utilisateur</param>
        /// <param name="dateDeNaissance">Date de naissance de l'utilisateur</param>
        public Amateur(string nom, string prenom, string pseudo, string motDePasse, string photoDeProfil, DateTime dateDeNaissance)
            : base(nom, pseudo, motDePasse, photoDeProfil)
        {
            Prenom = string.IsNullOrWhiteSpace(prenom) ? throw new ArgumentNullException("Le prénom d'un Amateur ne peut pas être nul",nameof(prenom)) : prenom;
            DateDeNaissance = dateDeNaissance;
            PhotosAimees = new ReadOnlyCollection<Photo>(photosAimees);
        }

        /// <summary>
        /// Constructeur d'un utilisateur Amateur avec Description
        /// </summary>
        /// <param name="nom">Nom de l'Amateur</param>
        /// <param name="prenom">Prenom de l'Amateur</param>
        /// <param name="pseudo">Pseudo de l'Amateur</param>
        /// <param name="motDePasse">Mot de passe de l'Amateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil de l'Amateur</param>
        /// <param name="description">Description du profil de l'Amateur</param>
        /// <param name="dateDeNaissance">Date de naissance de l'Amateur</param>
        public Amateur(string nom, string prenom, string pseudo, string motDePasse, string photoDeProfil, string description, DateTime dateDeNaissance)
            : base(nom, pseudo, motDePasse, photoDeProfil, description)
        {
            Prenom = string.IsNullOrWhiteSpace(prenom) ? throw new ArgumentNullException(nameof(prenom), "Le prénom d'un Amateur ne peut pas être nul") : prenom;
            DateDeNaissance = dateDeNaissance;
            PhotosAimees = new ReadOnlyCollection<Photo>(photosAimees);
        }

        /// <summary>
        /// Ajoute une photo dans la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à ajouter</param>
        public void AimerPhoto(Photo photo)
        {
            if (photo == null) throw new ArgumentNullException(nameof(photo),"La photo passé en paramètre est nulle");
            if (PhotosAimees.Contains(photo)) throw new InvalidPhotoException($"L'Amateur {ToShortString()} a déjà aimé la Photo {photo.Identifiant}");
            if (!photo.Proprietaire.MesPhotos.Contains(photo)) throw new InvalidPhotoException($"Le propiétaire de la photo n'a pas posté la photo {photo.Identifiant}");
            photosAimees.Add(photo);
            photo.AugmenterJaimes();
        }

        /// <summary>
        /// Supprime une photo de la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void NePlusAimerPhoto(string identifiant)
        {
            if (identifiant == null) throw new ArgumentNullException(nameof(identifiant),"L'identifiant passé en paramètre est nulle");

            Photo photo = photosAimees.Find(p => p.Identifiant == identifiant);
            if (photo == null) throw new InvalidPhotoException($"La photo associé à l'identifiant {identifiant} passé en paramètre n'est pas aimé par l'Amateur{ToShortString()}");
            photosAimees.Remove(photo);
            photo.DiminuerJaimes();
        }

        /// <summary>
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// selon les attributs choisis
        /// </summary>
        /// <param name="other">Objet à comparer</param>
        /// <returns>Renvoie vrai si égale, si non faux</returns>
        public bool Equals(Amateur other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Methode virtuelle de Equals
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// </summary>
        /// <param name="obj">Objet à comparer</param>
        /// <returns>Renvoie vrai si égale, si non faux</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((obj as Amateur));
        }

        /// <summary>
        /// Permet d'obtenir le HashCode de l'objet actuelle
        /// </summary>
        /// <returns>Retourne le HashCode de l'objet associé</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Permet d'obtenir une chaine de caractère qui représente l'objet
        /// </summary>
        /// <returns>Renvoie la chaine de caractère d'un Amateur</returns>
        public override string ToString() => $"{Nom} {Prenom}({Pseudo},{DateDeNaissance.ToShortDateString()})";

        /// <summary>
        /// Permet d'obtenir les informations essentielles d'un Amateur
        /// </summary>
        /// <returns>Renvoie la chaine de caractère d'un Amateur sous forme réduite</returns>
        public override string ToShortString() => $"{Nom} {Prenom}({Pseudo})";
    }
}
