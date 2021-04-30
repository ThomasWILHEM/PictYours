using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public abstract class Amateur : Utilisateur
    {
        /// <summary>
        /// Prenom de l'Amateur
        /// </summary>
        public string Prenom
        {
            get => prenom;
            set
            {
                if (value != null)
                    prenom = value;
            }
        }
        private string prenom;

        /// <summary>
        /// Date de naissance de l'Amateur
        /// </summary>
        public DateTime DateDeNaissance
        {
            get
            {
                return dateDeNaissance;
            }
            set
            {
                dateDeNaissance = value;
            }
        }
        private DateTime dateDeNaissance;

        /// <summary>
        /// Liste des photos aimées de l'utilisateur
        /// </summary>
        public List<Photo> PhotosAimees { get; private set; }

        /// <summary>
        /// Constructeur d'un utilisateur amateur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prenom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <param name="dateDeNaissance">Date de naissance de l'utilisateur</param>
        public Amateur(string nom,string prenom, string pseudo, string motDePasse,DateTime dateDeNaissance)
            : base(nom, pseudo, motDePasse)
        {
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
        }

        /// <summary>
        /// Ajoute une photo dans la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à ajouter</param>
        public void AimerPhoto(Photo photo) => PhotosAimees.Add(photo);

        /// <summary>
        /// Supprime une photo de la liste de photos aimées de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void NePlusAimerPhoto(Photo photo) => PhotosAimees.Remove(photo);
    }
}
