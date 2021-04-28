using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BiblioClasse
{
    public class Photo
    {
        /// <summary>
        /// Chemin de la photo
        /// </summary>
        public string CheminPhoto { get; private set; }

        /// <summary>
        /// Description de la photo
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Lieu où la photo à été prise
        /// </summary>
        public string Lieu { get; private set; }

        /// <summary>
        /// Utilisateur ayant posté la photo
        /// </summary>
        public Utilisateur Prorpiétaire { get; private set; }

        /// <summary>
        /// Date de publication
        /// </summary>
        public DateTime DatePub { get; private set; }

        /// <summary>
        /// Nombre de "Like" de la photo
        /// </summary>
        public int NBJaimes { get; private set; }

        /// <summary>
        /// Identifiant de la photo utilisé pour la retrouver facilement
        /// </summary>
        public string Identifiant { get; private set; }


        /// <summary>
        /// Constructeur d'une photo
        /// </summary>
        /// <param name="chemin"></param>
        /// <param name="description"></param>
        /// <param name="lieu"></param>
        /// <param name="proprietaire"></param>
        /// <param name="datepub"></param>
        /// <param name="nbJaimes"></param>
        /// <param name="identifiant"></param>
        public Photo(string chemin, string description, string lieu, Utilisateur proprietaire, DateTime datepub, int nbJaimes, string identifiant)
        {
            CheminPhoto = chemin;
            Description = description;
            Lieu = lieu;
            Prorpiétaire = proprietaire;
            DatePub = datepub;
            NBJaimes = nbJaimes;
            Identifiant = identifiant;
        }
    }
}