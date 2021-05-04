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
        public Categorie Categorie { get; private set; }


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
        public Photo(string cheminPhoto, string description, string lieu, Utilisateur proprietaire, DateTime datepub, int nbJaimes, string identifiant,Categorie categorie)
        {
            CheminPhoto = cheminPhoto ?? throw new ArgumentNullException(nameof(cheminPhoto));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Lieu = lieu ?? throw new ArgumentNullException(nameof(lieu));
            Proprietaire = proprietaire ?? throw new ArgumentNullException(nameof(proprietaire));
            DatePub = datepub;
            NbJaimes = nbJaimes;
            Identifiant = identifiant ?? throw new ArgumentNullException(nameof(identifiant));
            Categorie = categorie;
        }

        public Photo(string cheminPhoto, string description, string lieu, Utilisateur proprietaire, DateTime datePub, Categorie categorie)
        {
            CheminPhoto = cheminPhoto ?? throw new ArgumentNullException(nameof(cheminPhoto));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Lieu = lieu ?? throw new ArgumentNullException(nameof(lieu));
            Proprietaire = proprietaire ?? throw new ArgumentNullException(nameof(proprietaire));
            DatePub = datePub;
            Categorie = categorie;
        }

        public void AugmenterJaimes() => NbJaimes++;

        public void DiminuerJAimes() => NbJaimes -= NbJaimes > 0 ? 1 : 0;

        public bool Equals(Photo other)
        {
            return Identifiant.Equals(other.Identifiant);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals(obj as Photo);
        }

        public override int GetHashCode()
        {
            return Identifiant.GetHashCode();
        }
    }
}