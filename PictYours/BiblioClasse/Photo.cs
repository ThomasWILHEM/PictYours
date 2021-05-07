using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BiblioClasse
{
    public class Photo : IEquatable<Photo>
    {
        /// <summary>
        /// Chemin de la photo
        /// </summary>
        public string CheminPhoto { get; private set; }

        /// <summary>
        /// Description de la photo
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Lieu où la photo à été prise
        /// </summary>
        public string Lieu { get; private set; }

        /// <summary>
        /// Utilisateur ayant posté la photo
        /// </summary>
        public Utilisateur Proprietaire { get; private set; }

        /// <summary>
        /// Date de publication
        /// </summary>
        public DateTime DatePub { get; private set; }

        /// <summary>
        /// Nombre de "Like" de la photo
        /// </summary>
        public int NbJaimes { get; private set; }

        /// <summary>
        /// Identifiant de la photo utilisé pour la retrouver facilement
        /// </summary>
        public string Identifiant { get; private set; }

        /// <summary>
        /// Categorie de la photo
        /// </summary>
        public ECategorie Categorie { get; private set; }


        /// <summary>
        /// Constructeur d'une photo
        /// </summary>
        /// <param name="cheminPhoto"></param>
        /// <param name="description"></param>
        /// <param name="lieu"></param>
        /// <param name="proprietaire"></param>
        /// <param name="datepub"></param>
        /// <param name="nbJaimes"></param>
        /// <param name="identifiant"></param>
        public Photo(string cheminPhoto, string description, string lieu, Utilisateur proprietaire, DateTime datePub, int nbJaimes, string identifiant,ECategorie categorie)
                        : this(cheminPhoto, description, lieu, proprietaire, datePub, categorie)
        {
            NbJaimes = nbJaimes;
            Identifiant = string.IsNullOrWhiteSpace(identifiant) ? throw new ArgumentNullException(nameof(identifiant)) : identifiant;
        }

        /// <summary>
        /// Constructeur d'une photo sans identifiant en paramètre
        /// </summary>
        /// <param name="cheminPhoto"></param>
        /// <param name="description"></param>
        /// <param name="lieu"></param>
        /// <param name="proprietaire"></param>
        /// <param name="datePub"></param>
        /// <param name="categorie"></param>
        public Photo(string cheminPhoto, string description, string lieu, Utilisateur proprietaire, DateTime datePub, ECategorie categorie)
        {
            CheminPhoto = string.IsNullOrWhiteSpace(cheminPhoto) ? throw new ArgumentNullException(nameof(cheminPhoto)) : cheminPhoto;
            Description = description;
            Lieu = string.IsNullOrWhiteSpace(lieu) ? throw new ArgumentNullException(nameof(lieu)) : lieu;
            Proprietaire = proprietaire == null  ? throw new ArgumentNullException(nameof(proprietaire)) : proprietaire;
            DatePub = datePub;
            Categorie = categorie;
        }

        /// <summary>
        /// Augmente d'une unité le nombre de J'aimes sur la photo actuelle
        /// </summary>
        public void AugmenterJaimes() => NbJaimes++;

        /// <summary>
        /// Diminue d'une unité le nombre de J'aimes sur la photo actuelle
        /// </summary>
        public void DiminuerJaimes() => NbJaimes -= NbJaimes > 0 ? 1 : 0;

        /// <summary>
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// selon les attributs choisis
        /// </summary>
        /// <param name="other">Objet à comparer</param>
        /// <returns>Renvoie vrai si égale, si non faux</returns>
        public bool Equals(Photo other)
        {
            return Identifiant.Equals(other.Identifiant);
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
            return Equals(obj as Photo);
        }

        /// <summary>
        /// Permet d'obtenir le HashCode de l'objet actuelle
        /// </summary>
        /// <returns>Retourne le HashCode de l'objet actuelle</returns>
        public override int GetHashCode()
        {
            return Identifiant.GetHashCode();
        }
    }
}