using BiblioClasse;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractPersistance
{
    [DataContract]
    class DataToPersist
    {
        /// <summary>
        /// Liste ds utilisateurs
        /// </summary>
        [DataMember(Order = 0)]
        public List<Utilisateur> ListeUtilisateurs { get; set; } = new();

        /// <summary>
        /// Dictonnaire qui possède en clé un Utilisateur et en valeur la liste de ses photos
        /// </summary>
        [DataMember(Order = 1)]
        public Dictionary<Utilisateur, List<Photo>> PhotosParUtilisateurs { get; set; } = new();

        /// <summary>
        ///Dictionnaire qui possède en clé une photo et en valeur la liste des personnes qui ont aimées cette photo
        /// </summary>
        [DataMember(Order = 2)]
        public Dictionary<Photo, List<Amateur>> ListeUtilisateursParPhotosAimees { get; set; } = new();

        /// <summary>
        /// Prochain identifiant de photo
        /// </summary>
        [DataMember(Order = 3)]
        public int ProchainIdentifiant { get; set; }
    }
}