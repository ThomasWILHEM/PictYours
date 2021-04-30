using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class Utilisateur
    {
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom 
        {
            get => nom;
            set
            {
                if (value != null)
                    nom = value;
            }
        }
        private string nom;

        /// <summary>
        /// Pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get => $"@{pseudo}";
            set
            {
                if (value != null)
                    pseudo = value;
            }
        }
        private string pseudo;

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public string MotDePasse { get; set; }

        /// <summary>
        /// Si le booleen est vrai alors l'utilisateur est connecté si non faux
        /// </summary>
        public bool EstConnecte { get; set; }

        /// <summary>
        /// Description du profil de l'utilisateur
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Liste des photos de l'utilisateur
        /// </summary>
        public List<Photo> MesPhotos { get; private set; } = new List<Photo>();

        

        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="pseudo"></param>
        /// <param name="motDePasse"></param>
        public Utilisateur(string nom, string pseudo, string motDePasse)
        {
            Nom = nom ?? throw new ArgumentNullException(nameof(nom));
            Pseudo = string.IsNullOrWhiteSpace(pseudo) ? throw new ArgumentNullException(nameof(nom)) : pseudo;
            MotDePasse = motDePasse ?? throw new ArgumentNullException(nameof(motDePasse));
        }

        /// <summary>
        /// Ajoute une photo passee en paramètre dans la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        /// <returns>Retourne vrai si tout s'est bien passé si non faux</returns>
        public bool AjouterPhoto(Photo photo)
        {
            if (photo != null)
            {
                MesPhotos.Add(photo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Supprime une photo passee en paramètre de la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        /// <returns>Retourne vrai si tout s'est bien passé si non faux</returns>
        public bool SupprimerPhoto(Photo photo)
        {
            if (photo != null)
                return MesPhotos.Remove(photo);
            return false;
        }

        
    }
}
