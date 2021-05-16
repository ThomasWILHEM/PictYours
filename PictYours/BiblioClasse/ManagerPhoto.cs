using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class ManagerPhoto
    {
        //Dictionary<ECategorie, List<Photo>> dicoCategorie;

        //<Pseudo d'un utilisateur, Liste des identifiants des photos de l'utilisateur>
        //Dictionary<string, List<Photo>> photosUtilisateurs;
        internal List<Photo> PhotosUtilisateurs { get; private set; }

        Dictionary<Photo, List<string>> listeUtilisateursParPhotosAimees;

        public ManagerPhoto()
        {
            PhotosUtilisateurs = new List<Photo>();
            listeUtilisateursParPhotosAimees = new Dictionary<Photo, List<string>>();
        }

        public void PosterUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur.EstConnecte)
            {
                bool result = utilisateur.AjouterPhoto(photo);
                //Afficher un Dialog en fonction du resultat
            }
        }

        /// <summary>
        /// Supprime une photo pour l'utilisateur passé en paramètre et supprime toutes
        /// les photos aimées par les autres utilisateurs
        /// </summary>
        /// <param name="listeUtilisateurs">La liste de tous les utilisateurs</param>
        /// <param name="pseudo">Pseudo de l'utilisateur à qui appartient la photo à supprimer</param>
        /// <param name="identifiant">Identifiant de la photo à supprimer</param>
        public void SupprimerUnePhoto(List<Utilisateur> listeUtilisateurs,string pseudo, string identifiant)
        {
            //On recherche l'utilisateur dans la listeUtilisateurs
            //Utilisateur utilisateur = listeUtilisateurs.Find(u => u.Pseudo.Equals(pseudo));
            //if (utilisateur == null) return; //S'il n'est pas présent on retourne

            Photo photo = PhotosUtilisateurs.Find(photo => photo.Identifiant.Equals(identifiant));
            if (photo  == null) return;

            Utilisateur utilisateur = photo.Proprietaire;

            // On vérifie si l'identifiant de la photo passé en paramètre appartient bien à l'utilisateur passé en paramètre
            //if (!PhotosUtilisateurs.TryGetValue(pseudo, out List<Photo> listePhotoUtilisateur)) return; //Utilisateur présent dans le dico?
            
            
            if (utilisateur.EstConnecte)
            {
                //On supprime toutes les occurences de photos dans la liste des photos aimées des autres utilisateurs
                
                
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

        public void NePlusAimerUnePhoto(List<Utilisateur> listeUtilisateurs,Utilisateur utilisateur, string identifiant)
        {
            if (utilisateur is Amateur amateur)
            {
                bool result = amateur.NePlusAimerPhoto(identifiant);
                //Afficher un Dialog
            }
        }
    }
}
