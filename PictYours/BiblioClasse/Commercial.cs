using System;

namespace BiblioClasse
{
    public class Commercial : UtilisateurPrive, IEquatable<Commercial>
    {
        /// <summary>
        /// Nombre de visites sur le profil du compte commercial
        /// </summary>
        public int NombreDeVisites
        {
            get => nombreDeVisites;
            set
            {
                if(value != nombreDeVisites)
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

        public Commercial(string nom, string pseudo, string motDePasse, string photoDeProfil, int nombreDeVisite, string siteWeb, string description)
            : base(nom, pseudo, motDePasse, photoDeProfil, description)
        {
            NombreDeVisites = nombreDeVisite;
            SiteWeb = siteWeb ?? throw new ArgumentNullException(nameof(siteWeb), "Le site web ne peut pas être nul");
        }

        public Commercial(string nom, string pseudo, string motDePasse, string photoDeProfil, string siteWeb, string description)
            : base(nom, pseudo, motDePasse, photoDeProfil, description)
        {
            SiteWeb = siteWeb ?? throw new ArgumentNullException(nameof(siteWeb), "Le site web ne peut pas être nul");
        }

        public void MettreEnAvantUnePhoto(Photo photo)
        {
            if (photo == null) throw new ArgumentNullException(nameof(photo), "La photo à mettre en avant est nulle");

            throw new NotImplementedException();
        }

        public bool Equals(Commercial other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((obj as Commercial));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

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
