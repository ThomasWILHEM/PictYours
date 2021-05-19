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

        /// <summary>
        /// Permet de poster une photo sur le profil de l'utilisateur
        /// Ajoute la photo à MesPhotos de l'utilisateur
        /// Ajoute la photo à la liste correspondante de l'utilisateur dans PhotosUtilisateurs
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <param name="photo"></param>
        public void PosterUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur == null || photo == null) throw new ArgumentNullException("La photo et/ou l'utilisateur envoyé est/sont nuls");
            if (!utilisateur.EstConnecte) throw new InvalidOperationException($"L'utilisateur {utilisateur.ToShortString()} n'est pas connecté");
            if (utilisateur.MesPhotos.Contains(photo)) throw new Exception("La photo à déjà été postée");
               

            if (PhotosParUtilisateurs.ContainsKey(utilisateur))
            {
                PhotosParUtilisateurs.GetValueOrDefault(utilisateur)?.Add(photo);
            }
            else
            {
                PhotosParUtilisateurs.Add(utilisateur, new List<Photo> { photo });
            }
            utilisateur.AjouterPhoto(photo);
            //Afficher un Dialog en fonction du resultat
        }

        /// <summary>
        /// Supprime une photo
        /// Supprime la photo à MesPhotos de l'utilisateur
        /// Supprime la photo à la liste correspondante de l'utilisateur dans PhotosUtilisateurs
        /// Supprime toutes les photos aimées par les autres utilisateurs
        /// </summary>
        /// <param name="listeUtilisateurs">La liste de tous les utilisateurs</param>
        /// <param name="pseudo">Pseudo de l'utilisateur à qui appartient la photo à supprimer</param>
        /// <param name="identifiant">Identifiant de la photo à supprimer</param>
        public void SupprimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur == null || photo == null) throw new ArgumentNullException("La photo et/ou l'utilisateur envoyé est/sont nuls");
            if (!utilisateur.EstConnecte) throw new Exception($"L'utilisateur {utilisateur.ToShortString()} n'est pas connecté");

            if (!PhotosParUtilisateurs.TryGetValue(utilisateur, out List<Photo> listePhoto)) throw new Exception($"L'utilisateur {utilisateur.ToShortString()} n'existe pas dans le dictionnaire PhotosParUtilisateurs");
            if (!listePhoto.Remove(photo)) throw new InvalidOperationException($"La photo {photo.Identifiant} n'est pas présente dans la liste de photos de l'utilisateur {utilisateur.ToShortString()}");
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

        /// <summary>
        /// Ajoute au dictionnaire ListeUtilisateursParPhotosAimees pour la photo donnée en paramètre,l'utilisateur dans la liste correspondante
        /// </summary>
        /// <param name="utilisateur">Utilisateur qui a aimé la photo</param>
        /// <param name="photo">Photo qui a été aimé</param>
        public void AimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            Amateur amateur = utilisateur as Amateur; //Seul les amateur peuvent ne plus aimer une photo
            if (amateur == null || photo == null) throw new ArgumentNullException("La photo et/ou l'utilisateur envoyé est/sont nuls");
            if (!amateur.EstConnecte) throw new InvalidOperationException($"L'Amateur {amateur.ToShortString()} n'est pas connecté");
            if (amateur.PhotosAimees.Contains(photo)) throw new InvalidOperationException($"L'Amateur {amateur.ToShortString()} a déjà aimé la photo {photo.Identifiant}");
            if (!photo.Proprietaire.MesPhotos.Contains(photo)) throw new InvalidOperationException($"Le propiétaire de la photo n'a pas posté la photo {photo.Identifiant}");
                
            if (ListeUtilisateursParPhotosAimees.ContainsKey(photo))
            {
                ListeUtilisateursParPhotosAimees.GetValueOrDefault(photo)?.Add(amateur);
            }
            else
            {
                ListeUtilisateursParPhotosAimees.Add(photo, new List<Amateur> { amateur });
            }
            amateur.AimerPhoto(photo);
            //Afficher un Dialog
        }

        /// <summary>
        /// Supprime du dictionnaire ListeUtilisateursParPhotosAimees pour la photo donnée en paramètre,l'utilisateur dans la liste correspondante
        /// </summary>
        /// <param name="utilisateur">Utilisateur qui n'aime plus la photo</param>
        /// <param name="photo">Photo plus aimé</param>
        public void NePlusAimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur is Amateur amateur) //Seul les amateur peuvent ne plus aimer une photo
            {
                if (!ListeUtilisateursParPhotosAimees.ContainsKey(photo)) throw new Exception("La photo n'a pas été aimée, elle n'est pas dans le dictionnaire de photo aimée");
                if (!ListeUtilisateursParPhotosAimees.TryGetValue(photo, out List<Amateur> listeAmateur)) return;
                listeAmateur.Remove(amateur);
                if (listeAmateur.Count == 0) ListeUtilisateursParPhotosAimees.Remove(photo); //Supprime dans le dictionnaire si plus aucun j'aimes sur la photo
                amateur.NePlusAimerPhoto(photo.Identifiant);
                //Afficher un Dialog
            }
        }
    }
}