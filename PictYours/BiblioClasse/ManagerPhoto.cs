using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public partial class ManagerUtilisateur
    {
        public void ManPosterUnePhoto(Photo photo)
        {
            if (UtilisateurActuel.EstConnecte)
            {
                bool result = UtilisateurActuel.AjouterPhoto(photo);
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

        public void ManAimerUnePhoto(Photo photo)
        {
            if (UtilisateurActuel is Amateur amateur)
            {
                bool result = amateur.AimerPhoto(photo);
                //Afficher un Dialog
            }
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
