using BiblioClasse;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;

namespace JsonPersistance
{
    public class JsonPers : IPersistanceManager
    {
        public static string filePath = "../JSON";
        public static string fileName = "persistance.json";
        public static string PersFile { get; private set; } = Path.Combine(filePath, fileName);

        public JsonPers()
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        }

        public (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            var json = File.ReadAllText(PersFile);

            var data = JsonConvert.DeserializeObject<DataToPersist>(json, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                ContractResolver = new DictionaryAsArrayResolver()
            });
            return (data.ListeUtilisateurs, data.PhotosParUtilisateurs, data.ListeUtilisateursParPhotosAimees, data.ProchainIdentifiant);
        }

        public void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
        {
            DataToPersist data = new();
            data.ListeUtilisateurs = listeUtilisateur;
            data.PhotosParUtilisateurs = photosParUtilisateurs;
            data.ListeUtilisateursParPhotosAimees = listeUtilisateursParPhotosAimees;
            data.ProchainIdentifiant = prochainIdentifiant;

            DefaultContractResolver contractResolver = new DictionaryAsArrayResolver();
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver
            });
            File.WriteAllText(PersFile, json);
        }
    }
}
