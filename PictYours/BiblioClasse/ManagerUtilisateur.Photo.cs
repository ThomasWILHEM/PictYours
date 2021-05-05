using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public partial class ManagerUtilisateur
    {
        public void ManPosterUnePhoto(Photo p)
        {
            if (UtilisateurActuel.EstConnecte)
            {
                bool result = UtilisateurActuel.AjouterPhoto(p);
                //Afficher un Dialog en fonction du resultat
            }
        }

        public void ManSupprimerUnePhoto(string identifiant)
        {
            if (UtilisateurActuel.EstConnecte)
            {
                UtilisateurActuel.SupprimerPhoto(identifiant);
                //Afficher un Dialog en fonction du resultat
            }
        }

        public void ManAimerUnePhoto(string identifiant)
        {
            Amateur a = UtilisateurActuel as Amateur;
            if (a == null) return;
            bool result = a.AimerPhoto(identifiant);
            //Afficher un Dialog
        }

        public void ManNePlusAimerUnePhoto(string identifiant)
        {
            Amateur a = UtilisateurActuel as Amateur;
            if (a == null) return;
            bool result = a.NePlusAimerPhoto(identifiant);
            //Afficher un Dialog
        }
    }
}
