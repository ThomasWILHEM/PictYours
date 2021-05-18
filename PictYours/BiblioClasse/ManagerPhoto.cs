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

        public Dictionary<Utilisateur, List<Photo>> PhotosParUtilisateurs { get; private set; }

        public Dictionary<Photo, List<Amateur>> ListeUtilisateursParPhotosAimees { get; private set; }

        public ManagerPhoto()
        {
            PhotosParUtilisateurs = new Dictionary<Utilisateur, List<Photo>>();
            ListeUtilisateursParPhotosAimees = new Dictionary<Photo, List<Amateur>>();
        }

        public void PosterUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur == null ||
                photo == null ||
                !utilisateur.EstConnecte ||
                utilisateur.MesPhotos.Contains(photo)
                ) return;

            if (PhotosParUtilisateurs.ContainsKey(utilisateur))
            {
                PhotosParUtilisateurs.GetValueOrDefault(utilisateur)?.Add(photo);
            }
            else
            {
                PhotosParUtilisateurs.Add(utilisateur, new List<Photo> { photo });
            }
            bool result = utilisateur.AjouterPhoto(photo);
            //Afficher un Dialog en fonction du resultat
        }

        /// <summary>
        /// Supprime une photo pour l'utilisateur passé en paramètre et supprime toutes
        /// les photos aimées par les autres utilisateurs
        /// </summary>
        /// <param name="listeUtilisateurs">La liste de tous les utilisateurs</param>
        /// <param name="pseudo">Pseudo de l'utilisateur à qui appartient la photo à supprimer</param>
        /// <param name="identifiant">Identifiant de la photo à supprimer</param>
        public void SupprimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur == null || photo == null || !utilisateur.EstConnecte) return;

            if (!PhotosParUtilisateurs.TryGetValue(utilisateur, out List<Photo> listePhoto)) return;
            listePhoto.Remove(photo);
            if (listePhoto.Count == 0) PhotosParUtilisateurs.Remove(utilisateur);

            //Recupere la liste des utilisateurs qui ont aimé la photo
            //Si la liste est nulle alors personne n'a aimé la photo donc pas besoin de ne plus aimer
            if (ListeUtilisateursParPhotosAimees.TryGetValue(photo, out List<Amateur> utilisateursPhotoAimees))
            {
                //On supprime toutes les occurences de la photo dans la liste des photos aimées des autres utilisateurs
                for (int i = 0; i < utilisateursPhotoAimees.Count; i++)
                {
                    utilisateursPhotoAimees[i].NePlusAimerPhoto(photo.Identifiant);
                }
            }



            ListeUtilisateursParPhotosAimees.Remove(photo);

            //Supprime dans MesPhotos de l'utilisateur
            utilisateur.SupprimerPhoto(photo.Identifiant);

            //Afficher un Dialog en fonction du resultat
        }

        public void AimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            Amateur amateur = utilisateur as Amateur;
            if (amateur == null ||
                photo == null ||
                !amateur.EstConnecte ||
                amateur.PhotosAimees.Contains(photo) || //Verifie si la photo n'est pas déjà aimée
                !photo.Proprietaire.MesPhotos.Contains(photo) //Verifie si la photo est bien publié
                ) return;

            if (ListeUtilisateursParPhotosAimees.ContainsKey(photo))
            {
                ListeUtilisateursParPhotosAimees
                    .GetValueOrDefault(photo)
                    ?.Add(amateur);
            }
            else
            {
                ListeUtilisateursParPhotosAimees.Add(
                    photo,
                    new List<Amateur> { amateur }
                );
            }
            bool result = amateur.AimerPhoto(photo);
            //Afficher un Dialog
        }

        public void NePlusAimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur is Amateur amateur)
            {
                if (!ListeUtilisateursParPhotosAimees.ContainsKey(photo)) return;
                if (!ListeUtilisateursParPhotosAimees.TryGetValue(photo, out List<Amateur> listeAmateur)) return;
                listeAmateur.Remove(amateur);
                if (listeAmateur.Count == 0) ListeUtilisateursParPhotosAimees.Remove(photo); //Supprime dans le dictionnaire si plus aucun j'aimes sur la photo
                bool result = amateur.NePlusAimerPhoto(photo.Identifiant);
                //Afficher un Dialog
            }
        }
    }
}