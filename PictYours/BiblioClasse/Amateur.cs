using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class Amateur : Utilisateur, IEquatable<Amateur>
    {
        /// <summary>
        /// Prenom de l'Amateur
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
        /// Date de naissance de l'Amateur
        /// </summary>
        public DateTime DateDeNaissance { get; set; }

        /// <summary>
        /// Liste des photos aimées de l'utilisateur
        /// </summary>
        public List<Photo> PhotosAimees { get; private set; } = new List<Photo>();

        /// <summary>
        /// Constructeur d'un utilisateur amateur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prenom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <param name="dateDeNaissance">Date de naissance de l'utilisateur</param>
        public Amateur(string nom, string prenom, string pseudo, string motDePasse, DateTime dateDeNaissance)
            : base(nom, pseudo, motDePasse)
        {
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
        }

        /// <summary>
        /// Ajoute une photo dans la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à ajouter</param>
        public bool AimerPhoto(string identifiant)
        {
            Photo photo = PhotosAimees.Find(p => p.Identifiant == identifiant);
            if (photo != null)
            {
                PhotosAimees.Add(photo);
                photo.AugmenterJaimes();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Supprime une photo de la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public bool NePlusAimerPhoto(string identifiant)
        {
            Photo photo = PhotosAimees.Find(p => p.Identifiant == identifiant);
            if (photo != null)
            {
                PhotosAimees.Remove(photo);
                photo.DiminuerJaimes();
                return true;
            }
            return false;
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
    }
}
