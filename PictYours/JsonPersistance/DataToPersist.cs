using BiblioClasse;
using System.Collections.Generic;

namespace JsonPersistance
{
    internal class DataToPersist
    {
        /// <summary>
        /// Liste ds utilisateurs
        /// </summary>
        public List<Utilisateur> ListeUtilisateurs { get; set; } = new();

        /// <summary>
        /// Dictonnaire qui possède en clé un Utilisateur et en valeur la liste de ses photos
        /// </summary>
        public Dictionary<Utilisateur, List<Photo>> PhotosParUtilisateurs { get; set; } = new();

        /// <summary>
        ///Dictionnaire qui possède en clé une photo et en valeur la liste des personnes qui ont aimées cette photo
        /// </summary>
        public Dictionary<Photo, List<Amateur>> ListeUtilisateursParPhotosAimees { get; set; } = new();

        /// <summary>
        /// Prochain identifiant de photo
        /// </summary>
        public int ProchainIdentifiant { get; set; }
    }
}