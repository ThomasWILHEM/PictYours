using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public abstract class Utilisateur : IEquatable<Utilisateur>
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
        /// Pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get => $"@{pseudo}";
            set
            {
                if (value != null)
                    pseudo = value;
            }
        }
        private string pseudo;

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public string MotDePasse { get; set; }

        /// <summary>
        /// Si le booleen est vrai alors l'utilisateur est connecté si non faux
        /// </summary>
        public bool EstConnecte { get; set; }

        /// <summary>
        /// Chemin de la photo de profil de l'utilisateur
        /// </summary>
        public string PhotoDeProfil { get; private set; }

        /// <summary>
        /// Description du profil de l'utilisateur
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Liste des photos de l'utilisateur
        /// </summary>
        public List<Photo> MesPhotos { get; private set; } = new List<Photo>();

        

        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil</param>
        public Utilisateur(string nom, string pseudo, string motDePasse,string photoDeProfil)
        {
            Nom = string.IsNullOrWhiteSpace(nom) ? throw new ArgumentNullException(nameof(nom)) : nom;
            Pseudo = string.IsNullOrWhiteSpace(pseudo) ? throw new ArgumentNullException(nameof(pseudo)) : pseudo;
            MotDePasse = string.IsNullOrWhiteSpace(motDePasse) ? throw new ArgumentNullException(nameof(motDePasse)) : motDePasse;
            PhotoDeProfil = string.IsNullOrWhiteSpace(photoDeProfil) ? throw new ArgumentNullException(nameof(photoDeProfil)) : photoDeProfil;
        }

        /// <summary>
        /// Constructeur d'un utilisateur avec une description
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil</param>
        /// <param name="description">Description de l'utilisateur</param>
        public Utilisateur(string nom, string pseudo, string motDePasse, string photoDeProfil, string description)
            : this(nom, pseudo, motDePasse,photoDeProfil)
        {
            Description = description;
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
        public bool SupprimerPhoto(string identifiant)
        {
            Photo photo = MesPhotos.Find(p => p.Identifiant == identifiant);
            if (photo != null)
                return MesPhotos.Remove(photo);
            return false;
        }

        /// <summary>
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// selon les attributs choisis
        /// </summary>
        /// <param name="other">Objet à comparer</param>
        /// <returns>Renvoie vrai si égale, si non faux</returns>
        public bool Equals(Utilisateur other)
        {
            return Pseudo.Equals(other.Pseudo);
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
            return Equals((obj as Utilisateur));
        }

        /// <summary>
        /// Permet d'obtenir le HashCode de l'objet actuelle
        /// </summary>
        /// <returns>Retourne le HashCode de l'objet associé</returns>
        public override int GetHashCode()
        {
            return Pseudo.GetHashCode();
        }
    }
}
