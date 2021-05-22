using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    /// <summary>
    /// Définit les attributs communs d'un utilisateur
    /// </summary>
    public abstract class Utilisateur : IEquatable<Utilisateur>
    {
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom
        {
            get => nom;
            internal set
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
            get => pseudo;
            private set
            {
                if (value != null)
                    pseudo = value;
            }
        }
        private string pseudo;

        /// <summary>
        /// Si le booleen est vrai alors l'utilisateur est connecté si non faux
        /// </summary>
        public bool EstConnecte { get; set; }

        /// <summary>
        /// Chemin de la photo de profil de l'utilisateur
        /// </summary>
        public string PhotoDeProfil { get; internal set; }

        /// <summary>
        /// Description du profil de l'utilisateur
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Liste des photos de l'utilisateur
        /// </summary>
        public ReadOnlyCollection<Photo> MesPhotos { get; }
        private List<Photo> mesPhotos = new List<Photo>();

        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil</param>
        public Utilisateur(string nom, string pseudo, string photoDeProfil)
        {
            Nom = string.IsNullOrWhiteSpace(nom) ? throw new ArgumentNullException(nameof(nom)) : nom;
            Pseudo = string.IsNullOrWhiteSpace(pseudo) ? throw new ArgumentNullException(nameof(pseudo)) : pseudo;
            PhotoDeProfil = string.IsNullOrWhiteSpace(photoDeProfil) ? throw new ArgumentNullException(nameof(photoDeProfil)) : photoDeProfil;
            MesPhotos = new ReadOnlyCollection<Photo>(mesPhotos);
        }

        /// <summary>
        /// Constructeur d'un utilisateur avec une description
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil</param>
        /// <param name="description">Description de l'utilisateur</param>
        public Utilisateur(string nom, string pseudo, string photoDeProfil, string description)
            : this(nom, pseudo, photoDeProfil)
        {
            Description = description;
        }

        /// <summary>
        /// Ajoute une photo passee en paramètre dans la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void AjouterPhoto(Photo photo)
        {
            if (photo == null) throw new ArgumentNullException("La photo passé passé en paramètre est nul");
            if (MesPhotos.Contains(photo)) throw new InvalidPhotoException($"La photo {photo.Identifiant} à déjà été postée");
            mesPhotos.Add(photo);
        }

        /// <summary>
        /// Supprime une photo passee en paramètre de la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void SupprimerPhoto(string identifiant)
        {
            if (identifiant == null) throw new ArgumentNullException("L'identifiant passé en paramètre est nul");
            Photo photo = mesPhotos.Find(p => p.Identifiant == identifiant);
            if (photo == null) throw new InvalidPhotoException($"La photo associé à l'identifiant {identifiant} n'est pas présente dans la liste de photo de l'utilisateur {ToShortString()}");
            mesPhotos.Remove(photo);
        }

        /// <summary>
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// selon les attributs choisis
        /// </summary>
        /// <param name="other">Objet à comparer</param>
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
        public override int GetHashCode()
        {
            return Pseudo.GetHashCode();
        }

        /// <summary>
        /// Méthode ToString d'un utilisateur
        /// Permet d'afficher un utilisateur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Nom}({Pseudo}) Description:{Description}";
        }

        /// <summary>
        /// Permet d'obtenir les informations essentielles d'un Utilisateur
        /// </summary>
        /// <returns>Renvoie la chaine de caractère d'un Utilisateur sous forme réduite</returns>
        public virtual string ToShortString() => $"{Nom}({Pseudo})";
    }
}
