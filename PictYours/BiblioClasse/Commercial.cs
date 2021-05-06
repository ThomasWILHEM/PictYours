using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class Commercial : Utilisateur,IEquatable<Commercial>
    {
        /// <summary>
        /// Nombre de visites sur le profil du compte commercial
        /// </summary>
        public int NombreDeVisite { get; private set; }

        /// <summary>
        /// Lien du site web du profil commercial
        /// </summary>
        public string SiteWeb { get; private set; }

        public Commercial(string nom,string pseudo,string motDePasse, string photoDeProfil, int nombreDeVisite, string siteWeb)
            :base(nom,pseudo,motDePasse, photoDeProfil)
        {
            NombreDeVisite = nombreDeVisite;
            SiteWeb = siteWeb ?? throw new ArgumentNullException(nameof(siteWeb));
        }

        public Commercial(string nom, string pseudo, string motDePasse, string photoDeProfil, string siteWeb)
            :base(nom,pseudo,motDePasse,photoDeProfil)
        {
            SiteWeb = siteWeb ?? throw new ArgumentNullException(nameof(siteWeb));
        }

        public void PromulguerPhoto(Photo p)
        {
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
    }
}
