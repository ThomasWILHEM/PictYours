using BiblioClasse;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractPersistance
{
    [DataContract]
    class DataToPersist
    {
        [DataMember(Order = 0)]
        public List<Utilisateur> ListeUtilisateurs { get; set; } = new();

        [DataMember(Order = 1)]
        public Dictionary<Utilisateur, List<Photo>> PhotosParUtilisateurs { get; set; } = new();

        [DataMember(Order = 2)]
        public Dictionary<Photo, List<Amateur>> ListeUtilisateursParPhotosAimees { get; set; } = new();

        [DataMember(Order = 3)]
        public int ProchainIdentifiant { get; set; }
    }
}