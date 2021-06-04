using System;
using System.Runtime.Serialization;

namespace BiblioClasse
{
    [DataContract]
    public class Commercial : UtilisateurPrive, IEquatable<Commercial>
    {
        /// <summary>
        /// Nombre de visites sur le profil du compte commercial
        /// </summary>
        [DataMember(Order = 1)]
        public int NombreDeVisites
        {
            get => nombreDeVisites;
            set
            {
                if (value != nombreDeVisites && value > 0)
                {
                    nombreDeVisites = value;
                    OnPropertyChanged();
                }
            }
        }
        private int nombreDeVisites;

        /// <summary>
        /// Lien du site web du profil commercial
        /// </summary>
        [DataMember(Order = 0)]
        public string SiteWeb
        {
            get => siteWeb;
            internal set
            {
                if (value != null && value != siteWeb)
                {
                    siteWeb = value;
                    OnPropertyChanged();
                }
            }
        }
        private string siteWeb;

        public Commercial()
        {

        }

        /// <summary>
        /// Constructeur d'un Commercial
        /// </summary>
        /// <param name="nom">Nom du commercial</param>
        /// <param name="pseudo">Pseudo du commercial</param>
        /// <param name="motDePasse">Mot de passe du commercial</param>
        /// <param name="photoDeProfil">Photo de profil du commercial</param>
        /// <param name="siteWeb">Site web du commercial</param>
        /// <param name="description">Description du commecial</param>
        /// <param name="nombreDeVisite">Nombre de visite sur le profil du commercial</param>
        public Commercial(string nom, string pseudo, string motDePasse, string photoDeProfil, string siteWeb, string description, int nombreDeVisite = 0)
            : base(nom, pseudo, motDePasse, photoDeProfil, description)
        {
            NombreDeVisites = nombreDeVisite;
            SiteWeb = siteWeb ?? throw new ArgumentNullException(nameof(siteWeb), "Le site web ne peut pas être nul");
        }

        /// <summary>
        /// Cette méthode permet de mettre en avant une photo et de la mettre en tête de la liste des photos du commercial
        /// </summary>
        /// <param name="photo">Photo à mettre en avant</param>
        public void MettreEnAvantUnePhoto(Photo photo)
        {
            if (photo == null) throw new ArgumentNullException(nameof(photo), "La photo à mettre en avant est nulle");
            int index = MesPhotos.IndexOf(photo);
            if (index == -1) throw new InvalidPhotoException("La photo n'existe pas dans la liste des photos de l'utilisateur.");
            mesPhotos.Move(index, 0);
        }


        /// <summary>
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// selon les attributs choisis
        /// </summary>
        /// <param name="other">Objet à comparer</param>
        /// <returns>Renvoie vrai si égale, si non faux</returns>
        public bool Equals(Commercial other)
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
            return Equals((obj as Commercial));
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
        public override string ToString()
        {
            return $"{base.ToString()} Nombres de visites:{NombreDeVisites} SiteWeb:{SiteWeb}";
        }

        /// <summary>
        /// Permet d'obtenir les informations essentielles d'un Commercial
        /// </summary>
        /// <returns>Renvoie la chaine de caractère d'un Commercial sous forme réduite</returns>
        public override string ToShortString()
        {
            return base.ToShortString();
        }
    }
}
