using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class ManagerPhoto
    {
        Dictionary<ECategorie, List<Photo>> dicoCategorie;
        Dictionary<string, List<string>> dicoPhotosAimees;

        public ManagerPhoto()
        {
            dicoCategorie = new Dictionary<ECategorie, List<Photo>>();
        }

        public void PosterUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur.EstConnecte)
            {
                bool result = utilisateur.AjouterPhoto(photo);
                //Afficher un Dialog en fonction du resultat
            }
        }

        public void SupprimerUnePhoto(List<Utilisateur> listeUtilisateur,Utilisateur utilisateur, string identifiant)
        {
            if (utilisateur.EstConnecte)
            {
                //List<string> listePseudo = dicoPhotosAimees.First(s => s.Equals(identifiant)).Value;

                //listeUtilisateur.Where(u => u.Pseudo.Equals(identifiant));

                utilisateur.SupprimerPhoto(identifiant);
                //Afficher un Dialog en fonction du resultat
            }
        }

        public void AimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur is Amateur amateur)
            {
                bool result = amateur.AimerPhoto(photo);
                //Afficher un Dialog
            }
        }

        public void NePlusAimerUnePhoto(Utilisateur utilisateur, string identifiant)
        {
            if (utilisateur is Amateur amateur)
            {
                bool result = amateur.NePlusAimerPhoto(identifiant);
                //Afficher un Dialog
            }
        }
    }
}
