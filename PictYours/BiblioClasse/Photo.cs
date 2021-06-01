using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BiblioClasse
{
    [DataContract]
    public class Photo : IEquatable<Photo>, INotifyPropertyChanged
    {
        /// <summary>
        /// Prochain identifiant de photo
        /// </summary>
        public static int prochainIdentifiant { get; internal set; }

        /// <summary>
        /// Chemin de la photo
        /// </summary>
        [DataMember]
        public string CheminPhoto { get; private set; }

        /// <summary>
        /// Description de la photo
        /// </summary>
        [DataMember]
        public string Description { get; private set; }

        /// <summary>
        /// Lieu où la photo à été prise
        /// </summary>
        [DataMember]
        public string Lieu { get; private set; }

        /// <summary>
        /// Utilisateur ayant posté la photo
        /// </summary>
        [DataMember]
        public Utilisateur Proprietaire { get; private set; }

        /// <summary>
        /// Date de publication
        /// </summary>
        [DataMember]
        public DateTime DatePub { get; private set; }

        /// <summary>
        /// Nombre de "Like" de la photo
        /// </summary>
        [DataMember]
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
        [DataMember]
        public string Identifiant { get; private set; }

        /// <summary>
        /// Categorie de la photo
        /// </summary>
        [DataMember]
        public ECategorie Categorie { get; private set; }


        /// <summary>
        /// Evenement pour notifier à la vue qu'une propriété à changer
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Méthode associé à PropertyChanged
        /// </summary>
        /// <param name="propertyName">Nom de la propriété changée</param>
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

        /// <summary>
        /// Permet d'obtenir une chaine de caractère qui représente l'objet
        /// </summary>
        /// <returns>Renvoie la chaine de caractère d'une Photo</returns>
        public override string ToString()
        {
            return $"Identifiant:{Identifiant} posté le {DatePub.ToShortDateString()} à {Lieu} par {Proprietaire}";
        }

    }
}