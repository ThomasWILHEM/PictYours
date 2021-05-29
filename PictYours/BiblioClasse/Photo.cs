using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BiblioClasse
{
    public class Photo : IEquatable<Photo>, INotifyPropertyChanged
    {
        public static int prochainIdentifiant { get; private set; }

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
        public int NbJaimes
        {
            get => nbJaimes;
            internal set
            {
                if (value != -1)
                {
                    nbJaimes = value;
                    OnPropertyChanged();
                }
            }
        }
        private int nbJaimes;

        /// <summary>
        /// Identifiant de la photo utilisé pour la retrouver facilement
        /// </summary>
        public string Identifiant { get; private set; }

        /// <summary>
        /// Categorie de la photo
        /// </summary>
        public ECategorie Categorie { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// Constructeur d'une photo
        /// </summary>
        /// <param name="cheminPhoto">Chemin vers la photo</param>
        /// <param name="description">Description de la photo</param>
        /// <param name="lieu">Lieu de la photo</param>
        /// <param name="proprietaire">Propriétaire de la photo</param>
        /// <param name="datePub">Date de publication de la photo</param>
        /// <param name="categorie">Catégorie de la photo</param>
        public Photo(string cheminPhoto, string description, string lieu, Utilisateur proprietaire, DateTime datePub, ECategorie categorie)
        {
            CheminPhoto = string.IsNullOrWhiteSpace(cheminPhoto) ? throw new ArgumentNullException(nameof(cheminPhoto)) : cheminPhoto;
            Description = description;
            Lieu = string.IsNullOrWhiteSpace(lieu) ? throw new ArgumentNullException(nameof(lieu)) : lieu;
            Proprietaire = proprietaire == null ? throw new ArgumentNullException(nameof(proprietaire)) : proprietaire;
            DatePub = datePub;
            Categorie = categorie;
            prochainIdentifiant++;
            Identifiant = $"p{prochainIdentifiant}";
        }

        /// <summary>
        /// Constructeur d'une photo
        /// </summary>
        /// <param name="cheminPhoto">Chemin vers la photo</param>
        /// <param name="description">Description de la photo</param>
        /// <param name="lieu">Lieu de la photo</param>
        /// <param name="proprietaire">Propriétaire de la photo</param>
        /// <param name="datePub">Date de publication de la photo</param>
        /// <param name="categorie">Catégorie de la photo</param>
        public Photo(string extension, string description, string lieu, Utilisateur proprietaire, ECategorie categorie)
        {
            Description = description;
            Lieu = string.IsNullOrWhiteSpace(lieu) ? throw new ArgumentNullException(nameof(lieu)) : lieu;
            Proprietaire = proprietaire == null ? throw new ArgumentNullException(nameof(proprietaire)) : proprietaire;
            DatePub = DateTime.Now;
            Categorie = categorie;
            prochainIdentifiant++;
            Identifiant = $"p{prochainIdentifiant}";

            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(extension));
            }
            else
            {

                CheminPhoto = $"{Identifiant}{extension}";
            }
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

        public override string ToString()
        {
            return $"Identifiant:{Identifiant} posté le {DatePub.ToShortDateString()} à {Lieu} par {Proprietaire}";
        }

    }
}