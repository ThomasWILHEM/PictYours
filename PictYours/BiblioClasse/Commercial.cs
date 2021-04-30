using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    class Commercial : Utilisateur
    {
        /// <summary>
        /// Nombre de visites sur le profil du compte commercial
        /// </summary>
        public int NombreDeVisite { get; private set; }

        /// <summary>
        /// Lien du site web du profil commercial
        /// </summary>
        public string SiteWeb { get; private set; }

        public Commercial(string nom,string pseudo,string motDePasse,int nombreDeVisite, string siteWeb)
            :base(nom,pseudo,motDePasse)
        {
            NombreDeVisite = nombreDeVisite;
            SiteWeb = siteWeb;
        }

        public void PromulguerPhoto(Photo p)
        {
            throw new NotImplementedException();
        }
    }
}
