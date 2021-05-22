using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    /// <summary>
    /// Le ManagerPhoto s'occupe de la gestion des interactions avec les photos
    /// </summary>
    public class ManagerPhoto
    {
        //Dictionary<ECategorie, List<Photo>> dicoCategorie;

        /// <summary>
        /// Dictonnaire qui possède en clé un Utilisateur et en valeur la liste de ses photos
        /// </summary>
        public Dictionary<Utilisateur, List<Photo>> PhotosParUtilisateurs { get; private set; }

        /// <summary>
        /// Dictionnaire qui possède en clé une photo et en valeur la liste des personnes qui ont aimées cette photo
        /// </summary>
        public Dictionary<Photo, List<Amateur>> ListeUtilisateursParPhotosAimees { get; private set; }

        /// <summary>
        /// Constructeur du ManagerPhoto
        /// </summary>
        public ManagerPhoto()
        {
            PhotosParUtilisateurs = new Dictionary<Utilisateur, List<Photo>>();
            ListeUtilisateursParPhotosAimees = new Dictionary<Photo, List<Amateur>>();
        }

        /// <summary>
        /// Permet de poster une photo sur le profil de l'Utilisateur
        /// Ajoute la photo à la liste MesPhotos de l'Utilisateur
        /// Ajoute la photo à la liste correspondante de l'utilisateur dans PhotosUtilisateurs
        /// </summary>
        /// <param name="utilisateur">Utilisateur qui poste la photo</param>
        /// <param name="photo">Photo à poster</param>
        public void PosterUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur == null || photo == null) throw new ArgumentNullException("La photo et/ou l'utilisateur envoyé est/sont nul(le.s)");
            if (!utilisateur.EstConnecte) throw new InvalidUserException($"L'utilisateur {utilisateur.ToShortString()} n'est pas connecté");
            if (utilisateur.MesPhotos.Contains(photo)) throw new InvalidPhotoException($"La photo {photo.Identifiant} à déjà été postée");

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
        /// Supprime toutes les photos aimées par les autres utilisateurs (ListeUtilisateursParPhotosAimees)
        /// </summary>
        /// <param name="utilisateur">Utilisateur qui supprime une photo</param>
        /// <param name="photo">Photo à supprimer</param>
        public void SupprimerUnePhoto(Utilisateur utilisateur, Photo photo)
        {
            if (utilisateur == null || photo == null) throw new ArgumentNullException("La photo et/ou l'utilisateur envoyé est/sont nuls");
            if (!utilisateur.EstConnecte) throw new InvalidUserException($"L'utilisateur {utilisateur.ToShortString()} n'est pas connecté");

            if (!PhotosParUtilisateurs.TryGetValue(utilisateur, out List<Photo> listePhoto)) throw new InvalidUserException($"L'utilisateur {utilisateur.ToShortString()} n'existe pas dans le dictionnaire PhotosParUtilisateurs");
            if (!listePhoto.Remove(photo)) throw new InvalidPhotoException($"La photo {photo.Identifiant} n'est pas présente dans la liste de photos de l'utilisateur {utilisateur.ToShortString()}");
            if (listePhoto.Count == 0) PhotosParUtilisateurs.Remove(utilisateur);

            //Recupere la liste des utilisateurs qui ont aimé la photo
            //Si la liste est nulle alors personne n'a aimé la photo donc pas besoin de ne plus aimer
            if (ListeUtilisateursParPhotosAimees.TryGetValue(photo, out List<Amateur> utilisateursPhotoAimees))
            {
                //On supprime toutes les occurences de la photo dans la liste des photos aimées des autres utilisateurs
                foreach(var u in utilisateursPhotoAimees)
                {
                    u.NePlusAimerPhoto(photo.Identifiant);
                }
                //for (int i = 0; i < utilisateursPhotoAimees.Count; i++)
                //{
                //    utilisateursPhotoAimees[i].NePlusAimerPhoto(photo.Identifiant);
                //}
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
            Amateur amateur = utilisateur as Amateur; //Seul les amateurs peuvent aimer une photo
            if (amateur == null || photo == null) throw new ArgumentNullException("La photo et/ou l'utilisateur envoyé est/sont nuls");
            if (!amateur.EstConnecte) throw new InvalidUserException($"L'Amateur {amateur.ToShortString()} n'est pas connecté");
            if (amateur.PhotosAimees.Contains(photo)) throw new InvalidPhotoException($"L'Amateur {amateur.ToShortString()} a déjà aimé la photo {photo.Identifiant}");
            if (!photo.Proprietaire.MesPhotos.Contains(photo)) throw new InvalidPhotoException($"Le propiétaire de la photo n'a pas posté la photo {photo.Identifiant}");
                
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
            if (utilisateur is Amateur amateur) //Seul les amateurs peuvent ne plus aimer une photo
            {
                if (!ListeUtilisateursParPhotosAimees.ContainsKey(photo)) throw new InvalidPhotoException("La photo n'a pas été aimée, elle n'est pas dans le dictionnaire de photo aimée");
                if (!ListeUtilisateursParPhotosAimees.TryGetValue(photo, out List<Amateur> listeAmateur)) return;
                listeAmateur.Remove(amateur);
                if (listeAmateur.Count == 0) ListeUtilisateursParPhotosAimees.Remove(photo); //Supprime dans le dictionnaire si plus aucun j'aimes sur la photo
                amateur.NePlusAimerPhoto(photo.Identifiant);
                //Afficher un Dialog
            }
        }
    }
}